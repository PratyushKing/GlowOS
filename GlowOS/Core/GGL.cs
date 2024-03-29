﻿using Cosmos.Core;
using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace GlowOS.Core
{
    public static class GGL
    {

    }

    public class GlowCanvas
    {
        public Bitmap data;
        public int width;
        public int height;

        public GlowCanvas(int width, int height)
        {
            this.width = width;
            this.height = height;
            data = new((uint)width, (uint)height, ColorDepth.ColorDepth32);
        }
        public GlowCanvas(int width, int height, Color backgroundColor)
        {
            this.width = width;
            this.height = height;
            data = new((uint)width, (uint)height, ColorDepth.ColorDepth32);
            Clear(backgroundColor);
        }

        public Bitmap getData() { return data; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
        public int getSize() { return width * height; }

        public Color this[uint Index]
        {
            get
            {
                // Check if index is out of bounds.
                if (Index >= getSize())
                {
                    return Color.Black;
                }

                return Color.FromArgb(data.RawData[Index]);
            }
            set
            {
                if (Index >= getSize())
                {
                    return;
                }

                if (value.A < 255)
                {
                    value = BlendAlphaColors(this[Index], value);
                }

                data.RawData[Index] = value.ToArgb();
            }
        }

        public Color this[int x, int y]
        {
            get
            {
                return Color.FromArgb(data.RawData[(Math.Clamp(y, 0, height) * Math.Clamp(width, 0, Kernel.Width)) + Math.Clamp(x, 0, width)]);
            }
            set
            {
                // Blend 2 colors together if the new color has alpha.
                if (value.A < 255)
                {
                    value = BlendAlphaColors(this[Math.Clamp(x, 0, width), Math.Clamp(y, 0, height)], value);
                }

                data.RawData[(y * width) + x] = value.ToArgb();
            }
        }

        public Color BlendAlphaColors(Color source, Color target)
        {
            if (target.A == 255)
                return target;
            if (target.A == 0)
                return source;
            
            // basic color blending algorithm (mostly) by Ivan Sutherland
            return Color.FromArgb(255, (int)target.R + (int)(source.R * target.A) >> 8, (int)target.G + (int)(source.G * target.A) >> 8, (int)target.B + (int)(source.B * target.A) >> 8);
        }

        public void Clear(Color color)
        {
            for (int cx = 0; cx < width; cx++)
            {
                for (int cy = 0; cy < height; cy++)
                {
                    this[cx, cy] = color;
                }
            }
        }

        public void Clear() => Clear(Color.Black);

        public void FillPos(Color color, int x, int y, int width, int height)
        {
            // basically clear but only a specific portion of the canvas.
            for (int cx = x; cx < width; cx++)
            {
                for (int cy = y; cy < height; cy++)
                {
                    this[cx, cy] = color;
                }
            }
        }

        public void DrawPoint(Color color, int x, int y) => this[x, y] = color;
        
        public void DrawLine(Color color, int x1, int y1, int x2, int y2)
        {
            // Calculations done via the help of Bresenham's line drawing algorithm
            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;

            while (x1 != x2 || y1 != y2)
            {
                DrawPoint(color, x1, y1);

                int e2 = err;

                if (e2 > -dx) { err -= dy; x1 += sx; }
                if (e2 < dy) { err += dx; y1 += sy; }
            }
        }

        public void DrawArc(Color color, int x, int y, int width, int height, int startAngle = 0, int endAngle = 360)
        {
            if (width == 0 || height == 0)
                return;

            for (double Angle = startAngle; Angle < endAngle; Angle += 0.5) {
                
                double Angle1 = Math.PI * Angle / 180;
                int ix = (int)Math.Clamp(width * Math.Cos(Angle1), - width + 1, width - 1);
                int iy = (int)Math.Clamp(height * Math.Sin(Angle1), - height + 1, height - 1);

                DrawPoint(color, x + ix, y + iy);
            }
        }

        public void DrawFilledRectangle(Color color, int x, int y, int width, int height, int radius)
        {
            // If its a direct rectangle with no alpha colors and border radius, use the "super-saiyan-fast" copy buffer method
            if (color.A == 255 && radius == 0)
            {
                int startX = Math.Max(x, 0);
                int startY = Math.Max(y, 0);
                int endX = Math.Min(x + width, this.width);
                int endY = Math.Min(y + height, this.height);

                // crop it and find size
                int croppedHeight = endY - startY;
                int croppedWidth = endX - startX;

                // get the final pos offset for the starting point?? man wtf is this blog
                int destination = (startY * this.width) + startX;
                FillPos(color, startX, startY, croppedWidth, croppedHeight);

                return; // so it doesnt redraw others
            }

            // If needs alpha but there's no radius.
            if (radius == 0)
            {
                // Cannot use FillPos here due to the calculation required every frame.
                int cx = 0, cy = 0;
                for (int cp = 0; cp < width * height; cp++) // cp = current pos
                {
                    cx = cp % width;
                    cy = cp / width;
                    this[x + cx, y + cy] = color;
                }
                return;
            }

            // If there's corner bordering then just use circles for corners :skull:
            // Also, no if statement is required cuz it returns if radius is 0 and its able to draw.

            // CASE 1: if height is twice the radius then only 2 circles.
            if (height == radius * 2)
            {
                DrawFilledCircle(color, x + radius, y + radius, radius);
                DrawFilledCircle(color, x + width + radius, y + radius, radius);
                DrawFilledRectangle(color, x + radius, y, width, height, 0); // oh am i a such a great genius.
                return; // so it doesnt overlap
            }
            
            // CASE 2: if width is twice the radius then also.... only 2 circles
            if (width == radius * 2)
            {
                DrawFilledCircle(color, x + radius, y + radius, radius);
                DrawFilledCircle(color, x + width + radius, y + radius, radius);
                DrawFilledRectangle(color, x, y + radius, width, height, 0);
                return; // so it doesnt overlap
            }

            // CASE 3: where there's enough space to properly draw a rounded rectangle.
            DrawFilledCircle(color, x + radius, y + radius, radius);
            DrawFilledCircle(color, x + width - radius - 1, y + radius, radius);

            DrawFilledCircle(color, x + radius, y + height - radius - 1, radius);
            DrawFilledCircle(color, x + width - radius - 1, y + height - radius - 1, radius);

            DrawFilledRectangle(color, x + radius, y, width - (radius * 2), height, 0);
            DrawFilledRectangle(color, x, y + radius, width, height - (radius * 2), 0);
        }

        public void DrawFilledCircle(Color color, int x, int y, int radius)
        {
            if (radius == 0)
                return;

            for (int ix = -radius; ix < radius; ix++)
            {
                int height = (int)Math.Sqrt((radius ^ 2) - (ix ^ 2));

                for (int iy = -height; iy < height; iy++)
                    this[ix + x, iy + y] = color;
            }
        }
    }
}

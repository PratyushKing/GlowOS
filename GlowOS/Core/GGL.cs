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

        public Bitmap getData() { return data; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }

        
        public void DrawPoint(Color color, int x, int y) => data.RawData[y * width + x] = ((color.R << 16) + (color.G << 8) + (color.B));
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
    }
}

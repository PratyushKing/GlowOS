using System;
using Cosmos.System.Graphics;
using CosmosTTF;

namespace GlowOS.Core
{
    public class CTTFGGLS : ITTFSurface // CosmosTTF Glow Graphics Library Surface
    {
        public GlowCanvas assignedCanvas;

        public CTTFGGLS(GlowCanvas canvas) => assignedCanvas = canvas;

        public void DrawBitmap(Bitmap bmp, int x, int y) => assignedCanvas.DrawBitmap(bmp, x, y);
    }
}

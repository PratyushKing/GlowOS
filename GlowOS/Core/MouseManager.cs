using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Core
{
    public static class MouseManager
    {
        public static void Update()
        {
            // this is the most I could optimise to keep cursor in bounds.
            Kernel.canvas.DrawImageAlpha(ResourceMgr.cursor, Math.Clamp((int)Cosmos.System.MouseManager.X, 0, (int)(Kernel.Width - ResourceMgr.cursor.Width)), Math.Clamp((int)Cosmos.System.MouseManager.Y, 0, (int)(Kernel.Height - ResourceMgr.cursor.Height)));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Core
{
    public static class TaskBar
    {
        public const int MenuHeight = 20;
        public static GlowCanvas upperMenuCanvas = new(Kernel.Width, MenuHeight);

        public static GlowCanvas Update()
        {
            return upperMenuCanvas;
        }

        public static void PrepareBuffer()
        {

        }
    }
}

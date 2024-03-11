using Cosmos.System.Graphics;
using CosmosTTF;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Resources
{
    public static class Initializer
    {

        public static void InitializeAllResources()
        {
            ResourceMgr.mainRegularFont = new(ResourceMgr.mainRegularFont_raw);
            ResourceMgr.mainBoldFont = new(ResourceMgr.mainBoldFont_raw);

            ResourceMgr.background = new(ResourceMgr.background_raw);
            ResourceMgr.cursor = new(ResourceMgr.cursor_raw);
        }
    }
}

namespace GlowOS
{
    public static class ResourceMgr
    {

        [ManifestResourceStream(ResourceName = "GlowOS.Resources.Quicksand-Bold.ttf")] public static byte[] mainBoldFont_raw;
        public static TTFFont mainBoldFont;

        [ManifestResourceStream(ResourceName = "GlowOS.Resources.Quicksand-Regular.ttf")] public static byte[] mainRegularFont_raw;
        public static TTFFont mainRegularFont;

        [ManifestResourceStream(ResourceName = "GlowOS.Resources.Bitmaps.background.bmp")] public static byte[] background_raw;
        public static Bitmap background;

        [ManifestResourceStream(ResourceName = "GlowOS.Resources.Bitmaps.cursor.bmp")] public static byte[] cursor_raw;
        public static Bitmap cursor;
    }
}
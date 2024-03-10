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
    }
}
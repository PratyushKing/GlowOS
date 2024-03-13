using System.Drawing;

namespace GlowOS.Core
{
    public static class GlowColors
    {
        private static GlowTheme currentTheme = GlowTheme.Dark;

        public static Color FaintRed = Color.FromArgb(245, 224, 220);
        public static Color LightRed = Color.FromArgb(242, 205, 205);
        public static Color Pink = Color.FromArgb(245, 194, 231);
        public static Color Purple = Color.FromArgb(203, 166, 247);
        public static Color Red = Color.FromArgb(243, 139, 168);
        public static Color DarkRed = Color.FromArgb(235, 160, 172);

        public static Color Orange = Color.FromArgb(250, 179, 135);
        public static Color Yellow = Color.FromArgb(249, 226, 175);

        public static Color Green = Color.FromArgb(166, 227, 161);

        public static Color Turquoise = Color.FromArgb(148, 226, 213);
        public static Color Sky = Color.FromArgb(137, 220, 235);
        public static Color Sapphire = Color.FromArgb(116, 199, 236);
        public static Color Blue = Color.FromArgb(137, 180, 250);
        public static Color Lavender = Color.FromArgb(180, 190, 254);

        public static Color Text = Color.FromArgb(205, 214, 244);
        public static Color SubText1 = Color.FromArgb(205, 214, 244);
        public static Color SubText2 = Color.FromArgb(166, 173, 200);

        public static Color MainSurface = Color.FromArgb(49, 50, 68);
        public static Color Surface1 = Color.FromArgb(69, 71, 90);
        public static Color Surface2 = Color.FromArgb(88, 91, 112);

        public static Color MainOverlay = Color.FromArgb(108, 112, 134);
        public static Color Overlay1 = Color.FromArgb(127, 132, 156);
        public static Color Overlay2 = Color.FromArgb(147, 153, 178);

        public static void ChangeTheme(GlowTheme changeTo)
        {
            if (currentTheme == changeTo)
                return; // no change required

            Color mainOverlay = MainOverlay;
            Color overlay1 = Overlay1;
            Color overlay2 = Overlay2;

            MainOverlay = MainSurface;
            Overlay1 = Surface1;
            Overlay2 = Surface2;

            MainSurface = mainOverlay;
            Surface1 = overlay1;
            Surface2 = overlay2;
        }

    }

    public enum GlowTheme
    {
        Dark,
        Light
    }
}

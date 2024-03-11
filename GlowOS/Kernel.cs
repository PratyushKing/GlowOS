using Cosmos.Core.Memory;
using Cosmos.System.Graphics;
using GlowOS.Core;
using GlowOS.Resources;
using StardustOS.SDSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace GlowOS
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS fs;
        public static Canvas canvas;
        public const int Width = 1280;
        public const int Height = 720;
        public static Dictionary<string, string> BootConfig;

        protected override void BeforeRun()
        {
            Console.WriteLine("Welcome to GlowOS!");
            Initializer.InitializeAllResources();

            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            if (!Directory.Exists("0:\\GlowOS\\")) Directory.CreateDirectory("0:\\GlowOS\\");

            BootConfig = ConfigMan.FetchConfig("0:\\GlowOS\\boot.cfg");

            if (BootConfig.ContainsKey("STARTUPS"))
            {
                int totalStartups = int.Parse(BootConfig["STARTUPS"]);
                totalStartups += 1;
                BootConfig["STARTUPS"] = totalStartups.ToString();

                Console.WriteLine("Total startups = " + totalStartups.ToString());
            } else
            {
                BootConfig["STARTUPS"] = "1";

                Console.WriteLine("Total startups = 1");
            }

            if(BootConfig.ContainsKey("AUTOLOGIN") && BootConfig["AUTOLOGIN"] == "true")
            {

            }

            ConfigMan.SaveConfig("0:\\GlowOS\\boot.cfg", BootConfig);

            Console.WriteLine($"BootConfig elements count = {BootConfig.Count}");

            foreach (var item in BootConfig)
            {
                Console.WriteLine($"{item.Key}={item.Value}");
            }

            Sys.MouseManager.ScreenWidth = Width;
            Sys.MouseManager.ScreenHeight = Height;
            Sys.MouseManager.X = (Width / 2) - ResourceMgr.cursor.Width;
            Sys.MouseManager.Y = (Height / 2) - ResourceMgr.cursor.Height;
            canvas = FullScreenCanvas.GetFullScreenCanvas(new(Width, Height, ColorDepth.ColorDepth32));
            canvas.DrawImage(ResourceMgr.background, 0, 0);
            UpperMenu.PrepareBuffer();
        }

        public static int framesCount = 0;

        protected override void Run()
        {
            if (framesCount % 6 == 0)
                canvas.DrawImage(ResourceMgr.background, 0, 0);

            ProcessManager.Update();
            canvas.Display();
        }

        protected override void AfterRun() // only for emergencies or extreme fuck-ups
        {
            ProcessManager.StopAllProcesses();
            // kernel crash/panic UI
        }
    }
}

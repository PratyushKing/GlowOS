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
        Sys.FileSystem.CosmosVFS fs;
        public static Dictionary<string, string> BootConfig;

        protected override void BeforeRun()
        {
            Console.WriteLine("Welcome to GlowOS!");
            Initializer.InitializeAllResources();

            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            if (!Directory.Exists("0:/GlowOS/")) Directory.CreateDirectory("0:/GlowOS/");

            BootConfig = ConfigMan.FetchConfig("0:/GlowOS/boot.cfg");

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

            ConfigMan.SaveConfig("0:/GlowOS/boot.cfg", BootConfig);

            Console.WriteLine($"BootConfig elements count = {BootConfig.Count}");

            foreach (var item in BootConfig)
            {
                Console.WriteLine($"{item.Key}={item.Value}");
            }
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
    }
}

using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardustOS.SDSystem
{
    public static class ConfigMan
    {
        // Config Manager
        // .cfg files

        public static Dictionary<string, string> FetchConfig(string path, bool lowerCaseKeys = false)
        {
            if(!File.Exists(path)) return new Dictionary<string, string>();

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] split = line.Split("=");

                keyValuePairs.Add(lowerCaseKeys ? split[0].ToLower() : split[0], split[1]);
            }

            return keyValuePairs;
        }

        public static void SaveConfig(string path, Dictionary<string, string> config)
        {
            Dictionary<string, string> parsedConfig = new Dictionary<string, string>();

            foreach (var item in config)
            {
                parsedConfig.Add(item.Key.ToUpper(), item.Value);
            }

            List<string> lines = new List<string>(parsedConfig.Keys.Count);

            foreach (var item in parsedConfig)
            {
                lines.Add(item.Key + "=" + item.Value);
            }

            File.WriteAllLines(path, lines.ToArray());
        }
    }
}

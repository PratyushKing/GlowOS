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
        // .cfg

        public static Dictionary<string, string> Configs = new Dictionary<string, string>();

        public static Dictionary<string, Dictionary<string, string>> CachedConfigs = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 
        /// </summary>
        /// <!-- <typeparam name="T">Supported types: int, int[] (Split ','), bool, bool[] (Split ','), byte, byte[] (ASCII -> Byte[]), string (Default), string[] (Split ',')</typeparam> -->
        /// <param name="path"></param>
        /// <param name="lowerCaseKeys"></param>
        /// <returns></returns>
        /*public static Dictionary<string, T> FetchConfig<T>(string path, bool lowerCaseKeys = false)
        {
            Dictionary<string, T> keyValuePairs = new Dictionary<string, T>();

            string[] lines = File.ReadAllText(path).Split('\n');

            foreach (string line in lines)
            {
                string[] split = line.Split('=');

                switch (typeof(T).Name)
                {
                    case "Int32":
                        keyValuePairs.Add(lowerCaseKeys ? split[0].ToLower() : split[0], int.Parse(split[1]));
                        break;
                }

                //keyValuePairs.Add(lowerCaseKeys ? split[0].ToLower() : split[0], (T)Convert.ChangeType(split[1], typeof(T)));
            }

            return keyValuePairs;
        }*/

        public static Dictionary<string, string> FetchConfig(string path, bool lowerCaseKeys = false)
        {
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

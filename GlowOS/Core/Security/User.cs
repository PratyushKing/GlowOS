using StardustOS.SDSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Core.Security
{
    public class User
    {
        public string Name { get; private set; } = "";
        public string Username { get; private set; } = "";
        public Encryption.Pass Password { get; private set; } = null;
        public UserConfig Config { get; private set; } = null;

        public bool LoadUser(string username)
        {
            string _baseDir = @"0:\home\" + username;

            if (!Directory.Exists(_baseDir)) return false;

            // Some config files:
            // /auth.cfg - The authentication config (Contains information for the Encryption.Pass class and the Username, Name comes in Prefs)
            // The rest are encrypted using the key provided by the Encryption.Pass class
            // /prefs.cfg

            // Load the auth config

            Dictionary<string, string> authCfg = ConfigMan.FetchConfig(Path.Combine(_baseDir, "auth.cfg"));

            if (!authCfg.ContainsKey("USERNAME") || !authCfg.ContainsKey("KEY") || !authCfg.ContainsKey("PASSWORD")) return false;

            Username = authCfg["USERNAME"];
            Password = new Encryption.Pass(uint.Parse(authCfg["KEY"]), Encoding.Default.GetBytes(authCfg["PASSWORD"]));

            // Ignore attempting to load the user prefs

            return true;
        }

        public bool AttemptLogin(string username, string password)
        {
            string _baseDir = @"0:\home\" + username;

            // See if the user is even loaded and if not load them (If they exist)
            if (Password == null)
            {
                if (!LoadUser(username)) return false;
            }

            if(Password.Compare(password))
            {
                // We know the passwords match so we can continue to load prefs

                Config = new UserConfig();

                Dictionary<string, string> userPrefs = ConfigMan.FetchConfig(Path.Combine(_baseDir, "prefs.cfg"));

                foreach (var pref in userPrefs)
                {
                    switch (pref.Key)
                    {
                        case "PREFTHEME":
                            Config.PreferredTheme = int.Parse(pref.Value);
                            break;
                    }
                }

                return true;
            } else
            {
                return false;
            }
        }

        public class UserConfig
        {
            public int PreferredTheme;
        }
    }
}

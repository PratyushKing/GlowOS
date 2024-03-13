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

        public bool LoadUser(string username, string password)
        {
            if (!Directory.Exists(@"0:\User\" + username)) return false;

            // TODO: Finish this

            return true;
        }
    }
}

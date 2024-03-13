using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Core.Security
{
    public class UserMan
    {
        public User CurrentUser { get; private set; } = null;
        public void InitialiseService()
        {

        }

        public bool AttemptLogin(string username, string password)
        {
            return false;
        }
    }
}

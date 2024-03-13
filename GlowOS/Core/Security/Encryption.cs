using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Core.Security
{
    public static class Encryption
    {
        public class Pass
        {
            public uint Key { get; private set; } = 0; // Used for encrpytion of all user data (Dynamic)

            // Key changes based on a few things
            // Position of byte in the array:
            // - pos % 2 == 0 -> Key ^ 2
            // - pos % 4 == 0 -> Key ^ 6
            // Can add more, but these are the only two for now
            // Makes it more secure and less predictable
            // 
            // We have no access to basically anything at all so the best we have is a strong Caeser Cipher.

            public byte[] PassE { get; private set; } = { 0 };

            public Pass(uint Key, byte[] PassE)
            {
                this.Key = Key;
                this.PassE = PassE;
            }

            public bool Compare(string Password)
            {
                return EncryptBytes(Encoding.Default.GetBytes(Password), Key) == PassE;
            }

            public bool Change(string OldPassword, string NewPassword)
            {
                if (!Compare(OldPassword)) return false;

                PassE = EncryptBytes(Encoding.Default.GetBytes(NewPassword), Key);

                return true;
            }

            public void _ForceChange(string NewPassword)
            {
                PassE = EncryptBytes(Encoding.Default.GetBytes(NewPassword), Key);
            }
        }

        public static byte[] EncryptBytes(this byte[] data, uint key)
        {
            for (int i = 0; i < data.Length; i++)
            {
                uint _key = key;

                if(i % 2 == 0)
                {
                    _key ^= 2;
                } else if(i % 4 == 0)
                {
                    _key ^= 6;
                }

                data[i] = (byte)((data[i] + _key) % 256);
            }

            return data;
        }

        public static byte[] DecryptBytes(this byte[] data, uint key)
        {
            for (int i = 0; i < data.Length; i++)
            {
                uint _key = key;

                if (i % 2 == 0)
                {
                    _key ^= 2;
                }
                else if (i % 4 == 0)
                {
                    _key ^= 6;
                }

                data[i] = (byte)((data[i] - _key) % 256);
            }

            return data;
        }
    }
}

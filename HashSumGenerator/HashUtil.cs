using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace HashSumGenerator
{
    class HashUtil
    {
        public static String ToMd5(String input)
        {
            return generateHash(input, System.Security.Cryptography.MD5.Create());
        }

        public static String ToSha256(String input)
        { 
            return generateHash(input, System.Security.Cryptography.SHA256.Create());
        }

        private static String generateHash(String path, HashAlgorithm algorithm)
        {
  
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            byte[] hash = algorithm.ComputeHash(file);

            StringBuilder sbuilder = new StringBuilder();
            foreach (byte bite in hash)
            {//"X2"
                sbuilder.Append(bite.ToString("x2"));
            }
            return sbuilder.ToString();
        }
    }
}

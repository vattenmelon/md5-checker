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
        public const string MD5 = "MD5";
        public const string SHA256 = "SHA256";
        public static String ToMd5(FileStream file)
        {
            return generateHash(file, System.Security.Cryptography.MD5.Create());
        }

        public static String ToSha256(FileStream file)
        { 
            return generateHash(file, System.Security.Cryptography.SHA256.Create());
        }

        private static String generateHash(FileStream file, HashAlgorithm algorithm)
        {
            byte[] hash = algorithm.ComputeHash(file);

            StringBuilder sbuilder = new StringBuilder();
            foreach (byte bite in hash)
            {   //"X2"
                sbuilder.Append(bite.ToString("x2"));
            }
            return sbuilder.ToString();
        }
    }
}

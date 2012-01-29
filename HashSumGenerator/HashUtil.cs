namespace HashSumGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;
    
    public static class HashUtil
    {
 
        public static string Hash(HashAlgorithm algo, Stream file)
        {  
            file.Position = 0;
            return BitConverter.ToString(algo.ComputeHash(file)).Replace("-", String.Empty).ToLower();
        }
   
    }
}

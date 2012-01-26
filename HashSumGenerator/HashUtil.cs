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
    
    public enum Algorithm
    {
        MD5,
        SHA256
    }
    
    public static class HashUtil
    {
        private static Dictionary<Algorithm, ProcessHashAlgorithmDelegate> algos = new Dictionary<Algorithm, ProcessHashAlgorithmDelegate>()
        {
            {
                Algorithm.MD5, new ProcessHashAlgorithmDelegate(ToMd5)
            },
            {
                Algorithm.SHA256, new ProcessHashAlgorithmDelegate(ToSha256)
            }
        };
                
        private delegate string ProcessHashAlgorithmDelegate(FileStream file);
           
        public static string Hash(Algorithm algo, FileStream file)
        {   
            return algos[algo](file);
        }
        
        private static string ToMd5(FileStream file)
        {
            return GenerateHash(file, System.Security.Cryptography.MD5.Create());
        }

        private static string ToSha256(FileStream file)
        {  
            return GenerateHash(file, System.Security.Cryptography.SHA256.Create());
        }

        private static string GenerateHash(FileStream file, HashAlgorithm algorithm)
        {    
            // go to start of stream
            file.Position = 0;
            return BitConverter.ToString(algorithm.ComputeHash(file)).Replace("-", String.Empty).ToLower();
        }    
    }
}

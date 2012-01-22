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
    	public enum Algorithm{
    		MD5,
    		SHA256
    	}
    	private delegate String ProcessHashAlgorithmDelegate(FileStream file);
    	static Dictionary<HashUtil.Algorithm, ProcessHashAlgorithmDelegate> algos = new Dictionary<HashUtil.Algorithm, ProcessHashAlgorithmDelegate>()
		{
    		{ Algorithm.MD5, new ProcessHashAlgorithmDelegate(ToMd5)},
    		{ Algorithm.SHA256, new ProcessHashAlgorithmDelegate(ToSha256)},
		};
		public static String Hash(Algorithm algo, FileStream file)
		{   
			return algos[algo](file);
		}
        private static String ToMd5(FileStream file)
        {
            return generateHash(file, System.Security.Cryptography.MD5.Create());
        }

        private static String ToSha256(FileStream file)
        {  
            return generateHash(file, System.Security.Cryptography.SHA256.Create());
        }

        private static String generateHash(FileStream file, HashAlgorithm algorithm)
        {
            return BitConverter.ToString(algorithm.ComputeHash(file)).Replace("-", String.Empty).ToLower();
        }
    	
    }
}

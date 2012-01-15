using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HashSumGenerator
{
    class FileUtil
    {
        public static FileStream OpenFileStream(String fileName)
        {
            return new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HashSumGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            String path = args[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow f = new MainWindow();
            f.Label_Filename = path.Substring(path.LastIndexOf("\\") + 1);
            f.TextBox_MD5Text = HashUtil.ToMd5(path);
            f.TextBox_Sha256Text = HashUtil.ToSha256(path);
            Application.Run(f);
        }
    }
}
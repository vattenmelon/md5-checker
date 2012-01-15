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
            //String path = @"c:\eula.1028.txt";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow f = new MainWindow(path);
            Application.Run(f);
        }
    }
}
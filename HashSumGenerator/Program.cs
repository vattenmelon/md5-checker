﻿using System;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string path = String.Empty;
            if (args.Length == 0){
                OpenFileDialog a = new OpenFileDialog();
                a.ShowDialog();
                path = a.FileName;
            }else{
                path = args[0];
            }
               if (String.IsNullOrEmpty(path)){
                   MessageBox.Show("No file selected");
               }else{
                MainWindow f = new MainWindow(path);
                   Application.Run(f);
               }
        }
    }
}
﻿namespace HashSumGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    
    public partial class MainWindow : Form
    {
        private FileStream file;
        private Dictionary<HashUtil.Algorithm, TextBox> textbox = new Dictionary<HashUtil.Algorithm, TextBox>();
        private IList<HashUtil.Algorithm> jobbs = new List<HashUtil.Algorithm>()
        {
            {
                HashUtil.Algorithm.MD5
            },
            {
                HashUtil.Algorithm.SHA256
            }
        };
        
        public MainWindow(string filePath)
        {
            this.InitializeComponent();
            this.textbox.Add(HashUtil.Algorithm.MD5, this.textBox_MD5);
            this.textbox.Add(HashUtil.Algorithm.SHA256, this.textBox_Sha256);
            this.label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\", StringComparison.InvariantCulture) + 1);
            this.file = File.OpenRead(filePath);
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {     
            HashUtil.Algorithm algo = this.jobbs.First();
            this.jobbs.Remove(algo);
            e.Result = new Tuple<HashUtil.Algorithm, string>(algo, HashUtil.Hash(algo, this.file));
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
            Tuple<HashUtil.Algorithm, string> resultTuple = e.Result as Tuple<HashUtil.Algorithm, string>;
            this.textbox[resultTuple.Item1].Text = resultTuple.Item2;
            if (this.jobbs.Count > 0)
            {
                this.backgroundWorker1.RunWorkerAsync();
            }
            else 
            {
                this.file.Close();
                this.progressBar1.Hide();
            }     
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace HashSumGenerator
{
    public partial class MainWindow : Form
    {
        private String filePath;
        private FileStream file;
        Dictionary<HashUtil.Algorithm, TextBox> textbox = new Dictionary<HashUtil.Algorithm, TextBox>();
        IList<HashUtil.Algorithm> jobbs = new List<HashUtil.Algorithm>()
        {
        	{HashUtil.Algorithm.MD5},
        	{HashUtil.Algorithm.SHA256}
        };
        public MainWindow(String filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
            textbox.Add(HashUtil.Algorithm.MD5, textBox_MD5);
            textbox.Add(HashUtil.Algorithm.SHA256, textBox_Sha256);
            label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            file = File.OpenRead(filePath);
            backgroundWorker1.RunWorkerAsync(jobbs);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        { 	
        	HashUtil.Algorithm algo = jobbs.First();
        	jobbs.Remove(algo);
        	e.Result = new Tuple<HashUtil.Algorithm, String>(algo, HashUtil.Hash(algo, file));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
        	Tuple<HashUtil.Algorithm, String> resultTuple = e.Result as Tuple<HashUtil.Algorithm, String>;
            textbox[resultTuple.Item1].Text = resultTuple.Item2;
            if (jobbs.Count > 0){
            	backgroundWorker1.RunWorkerAsync();
            }
            else 
            {
                file.Close();
                progressBar1.Hide();
            }     
        }
        public String FilePath
        {
            set { filePath = value; }
        }
    }

}

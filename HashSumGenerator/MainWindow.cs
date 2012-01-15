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
        public MainWindow(String filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
            label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            backgroundWorker1.RunWorkerAsync(null);
        }

        public String FilePath
        {
            set { filePath = value; }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            FileStream file = FileUtil.OpenFileStream(filePath);
            result.Add(HashUtil.MD5, HashUtil.ToMd5(file));
            result.Add(HashUtil.SHA256, HashUtil.ToSha256(file));
            file.Close();
            e.Result = result;

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<String, String> resultObject = e.Result as  Dictionary<String, String>;
            textBox_MD5.Text = resultObject[HashUtil.MD5];
            textBox_Sha256.Text = resultObject[HashUtil.SHA256];
            progressBar1.Hide();
        }
    }
}

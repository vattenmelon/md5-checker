namespace HashSumGenerator
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
        private Dictionary<Algorithm, TextBox> textbox = new Dictionary<Algorithm, TextBox>();
        private IList<Algorithm> jobbs = new List<Algorithm>()
        {
            {
                Algorithm.MD5
            },
            {
                Algorithm.SHA256
            }
        };
        
        public MainWindow(string filePath)
        {
            this.InitializeComponent();
            this.textbox.Add(Algorithm.MD5, this.textBox_MD5);
            this.textbox.Add(Algorithm.SHA256, this.textBox_Sha256);
            this.label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\", StringComparison.InvariantCulture) + 1);
            this.file = File.OpenRead(filePath);
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {     
            Algorithm algo = this.jobbs.First();
            this.jobbs.Remove(algo);
            e.Result = new Tuple<Algorithm, string>(algo, HashUtil.Hash(algo, this.file));
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
            Tuple<Algorithm, string> resultTuple = e.Result as Tuple<Algorithm, string>;
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

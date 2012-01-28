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
        private string filePath;
        private IDictionary<Algorithm, BackgroundWorker> workers = new Dictionary<Algorithm, BackgroundWorker>();
        private Dictionary<Algorithm, TextBox> textbox = new Dictionary<Algorithm, TextBox>();
        private List<Algorithm> jobbs = new List<Algorithm>()
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
            this.filePath = filePath;
            this.textbox.Add(Algorithm.MD5, this.textBox_MD5);
            this.textbox.Add(Algorithm.SHA256, this.textBox_Sha256);
            this.label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\", StringComparison.InvariantCulture) + 1);
            
            CreateJobs();
            
            StartJobs();
            
        }

        void CreateJobs()
        {
            int maxNumberOfThreads = 2;
            int max = maxNumberOfThreads <= jobbs.Count ? maxNumberOfThreads : jobbs.Count;
            for (int i = 0; i < maxNumberOfThreads; i++)
            {   
                Algorithm alg = jobbs[i];
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += DoWork;
                worker.RunWorkerCompleted += WorkerCompleted;
                workers.Add(alg, worker);
            }
        }

        void StartJobs()
        {
            foreach (var entry in workers) {
                Tuple<Algorithm, Stream> arguments = new Tuple<Algorithm, Stream>(entry.Key, File.OpenRead(filePath));
                entry.Value.RunWorkerAsync(arguments);
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<Algorithm, Stream> arguments = e.Argument as Tuple<Algorithm, Stream>;
            Algorithm algo = arguments.Item1;
            Stream stream = arguments.Item2;
            e.Result = new Tuple<Algorithm, string, Stream>(algo, HashUtil.Hash(algo, stream), stream);
        }


        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Tuple<Algorithm, string, Stream> resultTuple = e.Result as Tuple<Algorithm, string, Stream>;
            resultTuple.Item3.Close();
            this.textbox[resultTuple.Item1].Text = resultTuple.Item2;

        }
    }
}

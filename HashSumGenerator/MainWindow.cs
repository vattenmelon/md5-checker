namespace HashSumGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
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
            },
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
            //int maxNumberOfThreads = 2;
            //int max = maxNumberOfThreads <= jobbs.Count ? maxNumberOfThreads : jobbs.Count;
            for (int i = 0; i < jobbs.Count; i++)
            {   
                Algorithm alg = jobbs[i];
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += DoWork;
                worker.RunWorkerCompleted += WorkerCompleted;
                workers.Add(alg, worker);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void StartJobs()
        {
            int maxSimultan = 4;
            int simult = maxSimultan <= workers.Count ? maxSimultan : workers.Count;
            System.Diagnostics.Debug.WriteLine("simultaneous: " + simult);
            for (int i = 0; i < simult; i++)
            {          
                var key = workers.Keys.ElementAt(i);
                System.Diagnostics.Debug.WriteLine("job: " + key + " , busy: " + workers[key].IsBusy);
                if (!workers[key].IsBusy)
                {                
                     System.Diagnostics.Debug.WriteLine("adding job: " + key);
                     Tuple<Algorithm, Stream> arguments = new Tuple<Algorithm, Stream>(key, File.OpenRead(filePath));
                     workers[key].RunWorkerAsync(arguments);   
                }
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
            RemoveWorker(resultTuple.Item1);
            StartJobs();

        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveWorker(Algorithm algorithm)
        {
            workers.Remove(algorithm);
            if (workers.Count == 0)
            {
                progressBar1.Hide();
            }
        }
        
        
        void MainWindowKeyUp(object sender, KeyEventArgs e)
        {  
            if (Keys.Enter.Equals(e.KeyCode))
            {
                this.Close();
            }
        }
    }
}

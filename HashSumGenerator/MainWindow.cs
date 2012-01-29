using System.Security.Cryptography;
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
        static readonly DateTime Epoch = new DateTime (1970, 1, 1);
        private long start;
        private string filePath;
        private IDictionary<HashAlgorithm, BackgroundWorker> workers = new Dictionary<HashAlgorithm, BackgroundWorker>();
        private Dictionary<HashAlgorithm, TextBox> textbox = new Dictionary<HashAlgorithm, TextBox>();
        private List<HashAlgorithm> jobbs = new List<HashAlgorithm>()
        {
            {
                MD5.Create()
            },
            {
                SHA256.Create()
            },
            
        };
        
        
        public MainWindow(string filePath)
        {
            this.InitializeComponent();
            this.filePath = filePath;
            this.textbox.Add(jobbs.Find( item => item is MD5), this.textBox_MD5);
            this.textbox.Add(jobbs.Find( item => item is SHA256), this.textBox_Sha256);
            this.label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\", StringComparison.InvariantCulture) + 1);
            this.start = CurrentTimeMillis();
            CreateJobs();
            
            StartJobs();
            
        }

        void CreateJobs()
        {
            jobbs.ForEach(algorithm => 
                          {
                            BackgroundWorker worker = new BackgroundWorker();
                            worker.DoWork += DoWork;
                            worker.RunWorkerCompleted += WorkerCompleted;
                            workers.Add(algorithm, worker);
                         });
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void StartJobs()
        {
            int maxSimultan = 1;
            int simult = maxSimultan <= workers.Count ? maxSimultan : workers.Count;
            System.Diagnostics.Debug.WriteLine("simultaneous: " + simult);
            for (int i = 0; i < simult; i++)
            {          
                var key = workers.Keys.ElementAt(i);
                System.Diagnostics.Debug.WriteLine("job: " + key + " , busy: " + workers[key].IsBusy);
                if (!workers[key].IsBusy)
                {                
                     System.Diagnostics.Debug.WriteLine("adding job: " + key);
                     Tuple<HashAlgorithm, Stream> arguments = new Tuple<HashAlgorithm, Stream>(key, File.OpenRead(filePath));
                     workers[key].RunWorkerAsync(arguments);   
                }
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<HashAlgorithm, Stream> arguments = e.Argument as Tuple<HashAlgorithm, Stream>;
            HashAlgorithm algo = arguments.Item1;
            Stream stream = arguments.Item2;
            e.Result = new Tuple<HashAlgorithm, string, Stream>(algo, HashUtil.Hash(algo, stream), stream);
        }


        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Tuple<HashAlgorithm, string, Stream> resultTuple = e.Result as Tuple<HashAlgorithm, string, Stream>;
            resultTuple.Item3.Close();
            this.textbox[resultTuple.Item1].Text = resultTuple.Item2;
            RemoveWorker(resultTuple.Item1);
            StartJobs();

        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveWorker(HashAlgorithm algorithm)
        {
            workers.Remove(algorithm);
            if (workers.Count == 0)
            {
                progressBar1.Hide();
                System.Diagnostics.Debug.WriteLine("calculating took: " + (CurrentTimeMillis() - start) + " ms.");
            }
        }
        
        
        void MainWindowKeyUp(object sender, KeyEventArgs e)
        {  
            if (Keys.Enter.Equals(e.KeyCode))
            {
                this.Close();
            }
        }
        
        static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow-Epoch).TotalMilliseconds;
        }
    }
}

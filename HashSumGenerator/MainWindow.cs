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
        IList<HashUtil.Algorithm> jobbs = new List<HashUtil.Algorithm>();
        public MainWindow(String filePath)
        {
            InitializeComponent();
            jobbs.Add(HashUtil.Algorithm.MD5);
            jobbs.Add(HashUtil.Algorithm.SHA256);
            this.filePath = filePath;
            textbox.Add(HashUtil.Algorithm.MD5, textBox_MD5);
            textbox.Add(HashUtil.Algorithm.SHA256, textBox_Sha256);
            label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            file = File.OpenRead(filePath);
            backgroundWorker1.RunWorkerAsync(jobbs);
        }

        public String FilePath
        {
            set { filePath = value; }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        { 	
        	IList<HashUtil.Algorithm> jobbs = e.Argument as IList<HashUtil.Algorithm>;
        	HashUtil.Algorithm algo = jobbs[0];
            Dictionary<HashUtil.Algorithm, String> resultDictionary = new Dictionary<HashUtil.Algorithm, String>();
            resultDictionary.Add(algo, HashUtil.Hash(algo, file));
            jobbs.Remove(algo);
            ResultObject res = new ResultObject();
            res.jobbs = jobbs;
            res.result = resultDictionary;
            e.Result = res;

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        	ResultObject resultObject = e.Result as ResultObject;
            Dictionary<HashUtil.Algorithm, String> resultDictionary = resultObject.result;
            HashUtil.Algorithm key = resultDictionary.Keys.First();
            textbox[key].Text = resultDictionary[key];
            if (resultObject.jobbs.Count > 0){
            	backgroundWorker1.RunWorkerAsync(resultObject.jobbs);
            }
            else 
            {
                file.Close();
                progressBar1.Hide();
            }
            
        }
    }
    class ResultObject{
    	public IList<HashUtil.Algorithm> jobbs;
    	public Dictionary<HashUtil.Algorithm, String> result;
    }
}

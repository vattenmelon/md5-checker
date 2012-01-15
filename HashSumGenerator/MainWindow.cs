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
        Dictionary<String, TextBox> textbox = new Dictionary<String, TextBox>();
        IList<String> algorithms = new List<String>();
        public MainWindow(String filePath)
        {
            InitializeComponent();
            algorithms.Add(HashUtil.MD5);
            algorithms.Add(HashUtil.SHA256);
            this.filePath = filePath;
            textbox.Add(HashUtil.MD5, textBox_MD5);
            textbox.Add(HashUtil.SHA256, textBox_Sha256);
            label_Filename.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            file = FileUtil.OpenFileStream(filePath);
            backgroundWorker1.RunWorkerAsync(algorithms);
        }

        public String FilePath
        {
            set { filePath = value; }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        { 	
        	IList<String> algos = e.Argument as IList<String>;
        	String algo = algos[0];
            Dictionary<String, String> resultDictionary = new Dictionary<String, String>();
            resultDictionary.Add(algo, HashUtil.Hash(algo, file));
            algos.Remove(algo);
            ResultObject res = new ResultObject();
            res.algos = algos;
            res.result = resultDictionary;
            e.Result = res;

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        	ResultObject resultObject = e.Result as ResultObject;
            Dictionary<String, String> resultDictionary = resultObject.result;
            String key = resultDictionary.Keys.First();
            textbox[key].Text = resultDictionary[key];
            if (resultObject.algos.Count > 0){
            	backgroundWorker1.RunWorkerAsync(resultObject.algos);
            }
            else 
            {
                file.Close();
                progressBar1.Hide();
            }
            
        }
    }
    class ResultObject{
    	public IList<String> algos;
    	public Dictionary<String, String> result;
    }
}

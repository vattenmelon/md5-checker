using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HashSumGenerator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public String TextBox_MD5Text
        {
            set { textBox_MD5.Text = value; }
        }

        public String TextBox_Sha256Text
        {
            set { textBox_Sha256.Text = value; }
        }

        public String Label_Filename
        {
            set { label_Filename.Text = value; }
        }
    }
}

namespace HashSumGenerator
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_MD5 = new System.Windows.Forms.Label();
            this.textBox_MD5 = new System.Windows.Forms.TextBox();
            this.label_Sha256 = new System.Windows.Forms.Label();
            this.textBox_Sha256 = new System.Windows.Forms.TextBox();
            this.label_Filename = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label_MD5
            // 
            this.label_MD5.AutoSize = true;
            this.label_MD5.Location = new System.Drawing.Point(12, 58);
            this.label_MD5.Name = "label_MD5";
            this.label_MD5.Size = new System.Drawing.Size(33, 13);
            this.label_MD5.TabIndex = 0;
            this.label_MD5.Text = "MD5:";
            // 
            // textBox_MD5
            // 
            this.textBox_MD5.Location = new System.Drawing.Point(15, 85);
            this.textBox_MD5.Name = "textBox_MD5";
            this.textBox_MD5.ReadOnly = true;
            this.textBox_MD5.Size = new System.Drawing.Size(453, 20);
            this.textBox_MD5.TabIndex = 1;
            // 
            // label_Sha256
            // 
            this.label_Sha256.AutoSize = true;
            this.label_Sha256.Location = new System.Drawing.Point(12, 123);
            this.label_Sha256.Name = "label_Sha256";
            this.label_Sha256.Size = new System.Drawing.Size(47, 13);
            this.label_Sha256.TabIndex = 2;
            this.label_Sha256.Text = "Sha256:";
            // 
            // textBox_Sha256
            // 
            this.textBox_Sha256.Location = new System.Drawing.Point(15, 149);
            this.textBox_Sha256.Name = "textBox_Sha256";
            this.textBox_Sha256.ReadOnly = true;
            this.textBox_Sha256.Size = new System.Drawing.Size(453, 20);
            this.textBox_Sha256.TabIndex = 3;
            // 
            // label_Filename
            // 
            this.label_Filename.AutoSize = true;
            this.label_Filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Filename.Location = new System.Drawing.Point(15, 13);
            this.label_Filename.Name = "label_Filename";
            this.label_Filename.Size = new System.Drawing.Size(41, 13);
            this.label_Filename.TabIndex = 4;
            this.label_Filename.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 192);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(453, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 5;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 229);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_Filename);
            this.Controls.Add(this.textBox_Sha256);
            this.Controls.Add(this.label_Sha256);
            this.Controls.Add(this.textBox_MD5);
            this.Controls.Add(this.label_MD5);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Hashgenerator";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindowKeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_MD5;
        private System.Windows.Forms.TextBox textBox_MD5;
        private System.Windows.Forms.Label label_Sha256;
        private System.Windows.Forms.TextBox textBox_Sha256;
        private System.Windows.Forms.Label label_Filename;
        private System.Windows.Forms.ProgressBar progressBar1;
        
    }
}


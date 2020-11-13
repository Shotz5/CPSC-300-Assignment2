using System.Windows.Forms;

namespace Assignment_2
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
            this.inputfile = new System.Windows.Forms.Button();
            this.outputfile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // inputfile
            // 
            this.inputfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inputfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.inputfile.Location = new System.Drawing.Point(408, 272);
            this.inputfile.Name = "inputfile";
            this.inputfile.Size = new System.Drawing.Size(218, 101);
            this.inputfile.TabIndex = 0;
            this.inputfile.Text = "Open Input File";
            this.inputfile.UseVisualStyleBackColor = true;
            this.inputfile.Click += new System.EventHandler(this.inputfile_Click);
            // 
            // outputfile
            // 
            this.outputfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.outputfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputfile.Location = new System.Drawing.Point(831, 584);
            this.outputfile.Name = "outputfile";
            this.outputfile.Size = new System.Drawing.Size(172, 48);
            this.outputfile.TabIndex = 1;
            this.outputfile.Text = "Output to .txt file";
            this.outputfile.UseVisualStyleBackColor = true;
            this.outputfile.Visible = false;
            this.outputfile.Click += new System.EventHandler(this.outputfile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1010, 620);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.MenuText;
            this.ClientSize = new System.Drawing.Size(1034, 644);
            this.Controls.Add(this.outputfile);
            this.Controls.Add(this.inputfile);
            this.Controls.Add(this.textBox1);
            this.Name = "MainWindow";
            this.Text = "Bank Line Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button inputfile;
        private Button outputfile;
        private TextBox textBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


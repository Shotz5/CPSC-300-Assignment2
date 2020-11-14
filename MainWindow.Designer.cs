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
            this.simoutput = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.finalsummary = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.simulating = new System.Windows.Forms.Label();
            this.finalsumm = new System.Windows.Forms.TextBox();
            this.simout = new System.Windows.Forms.Label();
            this.sumout = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputfile
            // 
            this.inputfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inputfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.inputfile.Location = new System.Drawing.Point(408, 278);
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
            this.outputfile.Text = "Output All to .txt file";
            this.outputfile.UseVisualStyleBackColor = true;
            this.outputfile.Visible = false;
            this.outputfile.Click += new System.EventHandler(this.outputfile_Click);
            // 
            // simoutput
            // 
            this.simoutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simoutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simoutput.Location = new System.Drawing.Point(12, 12);
            this.simoutput.MaxLength = 0;
            this.simoutput.Multiline = true;
            this.simoutput.Name = "simoutput";
            this.simoutput.ReadOnly = true;
            this.simoutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.simoutput.Size = new System.Drawing.Size(1010, 296);
            this.simoutput.TabIndex = 2;
            this.simoutput.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // finalsummary
            // 
            this.finalsummary.AutoSize = true;
            this.finalsummary.BackColor = System.Drawing.SystemColors.Window;
            this.finalsummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalsummary.Location = new System.Drawing.Point(354, 303);
            this.finalsummary.Name = "finalsummary";
            this.finalsummary.Padding = new System.Windows.Forms.Padding(5);
            this.finalsummary.Size = new System.Drawing.Size(326, 39);
            this.finalsummary.TabIndex = 3;
            this.finalsummary.Text = "Generating Final Summary...";
            this.finalsummary.Visible = false;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(389, 385);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(256, 37);
            this.progress.TabIndex = 4;
            this.progress.Visible = false;
            // 
            // simulating
            // 
            this.simulating.AutoSize = true;
            this.simulating.BackColor = System.Drawing.SystemColors.Window;
            this.simulating.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulating.Location = new System.Drawing.Point(440, 303);
            this.simulating.Name = "simulating";
            this.simulating.Padding = new System.Windows.Forms.Padding(5);
            this.simulating.Size = new System.Drawing.Size(154, 39);
            this.simulating.TabIndex = 5;
            this.simulating.Text = "Simulating...";
            this.simulating.Visible = false;
            // 
            // finalsumm
            // 
            this.finalsumm.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalsumm.Location = new System.Drawing.Point(12, 353);
            this.finalsumm.MaxLength = 0;
            this.finalsumm.Multiline = true;
            this.finalsumm.Name = "finalsumm";
            this.finalsumm.ReadOnly = true;
            this.finalsumm.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.finalsumm.Size = new System.Drawing.Size(1010, 279);
            this.finalsumm.TabIndex = 6;
            this.finalsumm.Visible = false;
            // 
            // simout
            // 
            this.simout.AutoSize = true;
            this.simout.BackColor = System.Drawing.SystemColors.Window;
            this.simout.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simout.Location = new System.Drawing.Point(12, 311);
            this.simout.Name = "simout";
            this.simout.Padding = new System.Windows.Forms.Padding(5);
            this.simout.Size = new System.Drawing.Size(230, 39);
            this.simout.TabIndex = 7;
            this.simout.Text = "^ Simulation Output";
            this.simout.Visible = false;
            // 
            // sumout
            // 
            this.sumout.AutoSize = true;
            this.sumout.BackColor = System.Drawing.SystemColors.Window;
            this.sumout.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sumout.Location = new System.Drawing.Point(822, 311);
            this.sumout.Name = "sumout";
            this.sumout.Padding = new System.Windows.Forms.Padding(5);
            this.sumout.Size = new System.Drawing.Size(200, 39);
            this.sumout.TabIndex = 8;
            this.sumout.Text = "v Final Summary";
            this.sumout.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.MenuText;
            this.ClientSize = new System.Drawing.Size(1034, 644);
            this.Controls.Add(this.sumout);
            this.Controls.Add(this.simout);
            this.Controls.Add(this.simulating);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.finalsummary);
            this.Controls.Add(this.outputfile);
            this.Controls.Add(this.inputfile);
            this.Controls.Add(this.simoutput);
            this.Controls.Add(this.finalsumm);
            this.Name = "MainWindow";
            this.Text = "Bank Line Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button inputfile;
        private Button outputfile;
        private TextBox simoutput;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label finalsummary;
        private ProgressBar progress;
        private Label simulating;
        private TextBox finalsumm;
        private Label simout;
        private Label sumout;
    }
}


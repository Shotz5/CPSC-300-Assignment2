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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.InputButton = new System.Windows.Forms.Button();
            this.OutputButton = new System.Windows.Forms.Button();
            this.SimOutput = new System.Windows.Forms.TextBox();
            this.ExecutionThread = new System.ComponentModel.BackgroundWorker();
            this.GenerateSummLabel = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.simulating = new System.Windows.Forms.Label();
            this.FinalSummOutput = new System.Windows.Forms.TextBox();
            this.SimOutputLabel = new System.Windows.Forms.Label();
            this.FinalSumLabel = new System.Windows.Forms.Label();
            this.CheckLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InputButton
            // 
            this.InputButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.InputButton.Location = new System.Drawing.Point(408, 366);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(218, 101);
            this.InputButton.TabIndex = 0;
            this.InputButton.Text = "Open Input File";
            this.InputButton.UseVisualStyleBackColor = true;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // OutputButton
            // 
            this.OutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputButton.Location = new System.Drawing.Point(831, 584);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(172, 48);
            this.OutputButton.TabIndex = 1;
            this.OutputButton.Text = "Output All to .txt file";
            this.OutputButton.UseVisualStyleBackColor = true;
            this.OutputButton.Visible = false;
            this.OutputButton.Click += new System.EventHandler(this.OutputButton_Click);
            // 
            // SimOutput
            // 
            this.SimOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SimOutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimOutput.Location = new System.Drawing.Point(12, 12);
            this.SimOutput.MaxLength = 0;
            this.SimOutput.Multiline = true;
            this.SimOutput.Name = "SimOutput";
            this.SimOutput.ReadOnly = true;
            this.SimOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SimOutput.Size = new System.Drawing.Size(1010, 296);
            this.SimOutput.TabIndex = 2;
            this.SimOutput.Visible = false;
            // 
            // ExecutionThread
            // 
            this.ExecutionThread.WorkerReportsProgress = true;
            this.ExecutionThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ExecutionThread_DoWork);
            this.ExecutionThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ExecutionThread_ProgressChanged);
            // 
            // GenerateSummLabel
            // 
            this.GenerateSummLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateSummLabel.AutoSize = true;
            this.GenerateSummLabel.BackColor = System.Drawing.SystemColors.Window;
            this.GenerateSummLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateSummLabel.Location = new System.Drawing.Point(354, 303);
            this.GenerateSummLabel.Name = "GenerateSummLabel";
            this.GenerateSummLabel.Padding = new System.Windows.Forms.Padding(5);
            this.GenerateSummLabel.Size = new System.Drawing.Size(326, 39);
            this.GenerateSummLabel.TabIndex = 3;
            this.GenerateSummLabel.Text = "Generating Final Summary...";
            this.GenerateSummLabel.Visible = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(389, 385);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(256, 37);
            this.ProgressBar.TabIndex = 4;
            this.ProgressBar.Visible = false;
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
            // FinalSummOutput
            // 
            this.FinalSummOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FinalSummOutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalSummOutput.Location = new System.Drawing.Point(12, 353);
            this.FinalSummOutput.MaxLength = 0;
            this.FinalSummOutput.Multiline = true;
            this.FinalSummOutput.Name = "FinalSummOutput";
            this.FinalSummOutput.ReadOnly = true;
            this.FinalSummOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FinalSummOutput.Size = new System.Drawing.Size(1010, 279);
            this.FinalSummOutput.TabIndex = 6;
            this.FinalSummOutput.Visible = false;
            // 
            // SimOutputLabel
            // 
            this.SimOutputLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SimOutputLabel.AutoSize = true;
            this.SimOutputLabel.BackColor = System.Drawing.SystemColors.Window;
            this.SimOutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimOutputLabel.Location = new System.Drawing.Point(12, 311);
            this.SimOutputLabel.Name = "SimOutputLabel";
            this.SimOutputLabel.Padding = new System.Windows.Forms.Padding(5);
            this.SimOutputLabel.Size = new System.Drawing.Size(230, 39);
            this.SimOutputLabel.TabIndex = 7;
            this.SimOutputLabel.Text = "^ Simulation Output";
            this.SimOutputLabel.Visible = false;
            // 
            // FinalSumLabel
            // 
            this.FinalSumLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FinalSumLabel.AutoSize = true;
            this.FinalSumLabel.BackColor = System.Drawing.SystemColors.Window;
            this.FinalSumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalSumLabel.Location = new System.Drawing.Point(822, 311);
            this.FinalSumLabel.Name = "FinalSumLabel";
            this.FinalSumLabel.Padding = new System.Windows.Forms.Padding(5);
            this.FinalSumLabel.Size = new System.Drawing.Size(200, 39);
            this.FinalSumLabel.TabIndex = 8;
            this.FinalSumLabel.Text = "v Final Summary";
            this.FinalSumLabel.Visible = false;
            // 
            // CheckLabel
            // 
            this.CheckLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckLabel.AutoSize = true;
            this.CheckLabel.BackColor = System.Drawing.SystemColors.Window;
            this.CheckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckLabel.Location = new System.Drawing.Point(422, 247);
            this.CheckLabel.Name = "CheckLabel";
            this.CheckLabel.Padding = new System.Windows.Forms.Padding(5);
            this.CheckLabel.Size = new System.Drawing.Size(189, 39);
            this.CheckLabel.TabIndex = 9;
            this.CheckLabel.Text = "Checking File...";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 93);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(475, 39);
            this.label1.TabIndex = 10;
            this.label1.Text = "This program takes as input a file of format";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(471, 132);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(92, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "(x,y)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(294, 169);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5);
            this.label3.Size = new System.Drawing.Size(447, 39);
            this.label3.TabIndex = 12;
            this.label3.Text = "Where x is the arrival time of a customer";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(241, 208);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5);
            this.label4.Size = new System.Drawing.Size(552, 39);
            this.label4.TabIndex = 13;
            this.label4.Text = "and y is the time the customer stays at the window";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(193, 247);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(648, 39);
            this.label5.TabIndex = 14;
            this.label5.Text = "The arrival times MUST be in order from lowest to greatest";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.MenuText;
            this.ClientSize = new System.Drawing.Size(1034, 644);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FinalSumLabel);
            this.Controls.Add(this.SimOutputLabel);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.OutputButton);
            this.Controls.Add(this.InputButton);
            this.Controls.Add(this.SimOutput);
            this.Controls.Add(this.FinalSummOutput);
            this.Controls.Add(this.CheckLabel);
            this.Controls.Add(this.simulating);
            this.Controls.Add(this.GenerateSummLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Bank Line Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button InputButton;
        private Button OutputButton;
        private TextBox SimOutput;
        private System.ComponentModel.BackgroundWorker ExecutionThread;
        private Label GenerateSummLabel;
        private ProgressBar ProgressBar;
        private Label simulating;
        private TextBox FinalSummOutput;
        private Label SimOutputLabel;
        private Label FinalSumLabel;
        private Label CheckLabel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}


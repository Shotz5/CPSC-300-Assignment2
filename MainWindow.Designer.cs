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
            this.SuspendLayout();
            // 
            // inputfile
            // 
            this.inputfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.inputfile.Location = new System.Drawing.Point(260, 192);
            this.inputfile.Name = "inputfile";
            this.inputfile.Size = new System.Drawing.Size(156, 41);
            this.inputfile.TabIndex = 0;
            this.inputfile.Text = "Open Input File";
            this.inputfile.UseVisualStyleBackColor = true;
            this.inputfile.Click += new System.EventHandler(this.inputfile_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuText;
            this.ClientSize = new System.Drawing.Size(676, 425);
            this.Controls.Add(this.inputfile);
            this.Name = "MainWindow";
            this.Text = "Bank Line Simulator";
            this.ResumeLayout(false);

        }

        #endregion

        private Button inputfile;
    }
}


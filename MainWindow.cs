using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;

namespace Assignment_2
{
    public partial class MainWindow : Form {
        public OpenFileDialog openFile;
        public MainWindow() {
            InitializeComponent();
        }

        private void inputfile_Click(object sender, EventArgs e) {
            openFile = new OpenFileDialog() {
                FileName = "BankFile.txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open Bank Lineup File"
            };

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK) {
                try {
                    var filePath = openFile.FileName;
                } catch (Exception eve) {
                    MessageBox.Show($"{eve.Message}\n\n {eve.StackTrace}");
                }
            } else if (result == DialogResult.Cancel) {
                MessageBox.Show("User cancelled Bank Line input");
            }
        }

    }
}

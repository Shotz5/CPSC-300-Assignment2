using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Forms;

public class OpenDialog : Form {
	public Button button;
	public OpenFileDialog openFileDiag;

	/*[STAThread]
	public static void Main() {
		Application.SetCompatibleTextRenderingDefault(false);
		Application.EnableVisualStyles();
		Application.Run(new OpenDialog());
	}*/

	public OpenDialog() {
		openFileDiag = new OpenFileDialog() {
			FileName = "Select a bank lineup input file",
			Filter = "Text files (*.txt)|*.txt",
			Title = "Open bank lineup file"
		};

		button = new Button() {
			Size = new Size(100, 20),
			Location = new Point(15, 15),
			Text = "Select File"
		};
		button.Click += new EventHandler(clickHandler);
		Controls.Add(button);
	}

	private void clickHandler(object sender, EventArgs e) {
		if (openFileDiag.ShowDialog() == DialogResult.OK) {
			try {
				var filePath = openFileDiag.FileName;
				using (Stream stream = openFileDiag.OpenFile()) {
					Process.Start("notepad.exe", filePath);
                } 
            } catch (SecurityException ex) {
				MessageBox.Show($"Security error. Error message: {ex.Message}\n\n" + $"Details: \n\n{ex.StackTrace}");
            }
        }
    }
}

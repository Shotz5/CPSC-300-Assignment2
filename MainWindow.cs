using System;
using System.IO;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {
        public OpenFileDialog openFile;
        Queue queue;
        EventList eventList;
        public MainWindow() {
            InitializeComponent();
            queue = new Queue();
            eventList = new EventList();
        }

        private void inputfile_Click(object sender, EventArgs e) {
            var filePath = "";
            openFile = new OpenFileDialog() {
                FileName = "BankFile.txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open Bank Lineup File"
            };

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK) {
                try {
                    filePath = openFile.FileName;
                } catch (Exception eve) {
                    MessageBox.Show($"{eve.Message}\n\n {eve.StackTrace}");
                }
            } else if (result == DialogResult.Cancel) {
                MessageBox.Show("User cancelled Bank Line input");
            }
            var button = (Button) sender;
            button.Visible = false;

            using (StreamReader stream = new StreamReader(filePath)) {
                string nextInput = "";
                while ((nextInput = stream.ReadLine()) != null) {
                    Person person = parsePerson(nextInput);
                    processArrival(person);
                }
            }
        }

        private Person parsePerson(String input) {
            string waitTime = "";
            string tellerTime = "";
            bool waitBool = true;
            // Format is (x,y) x - arrival time / y - teller time
            for (int i = 0; i < input.Length; i++) {
                if (Char.IsDigit(input[i]) && waitBool) {
                    waitTime += input[i];
                } else if (input[i] == ',') {
                    waitBool = false;
                } else if (Char.IsDigit(input[i]) && !waitBool) {
                    tellerTime += input[i];
                }
            }
            int intWaitTime = -1;
            int intTellerTime = -1;
            try {
                intWaitTime = Int32.Parse(waitTime);
                intTellerTime = Int32.Parse(tellerTime);
            } catch (Exception e) {
                // Sourced from https://stackoverflow.com/questions/2109441/how-to-show-error-warning-message-box-in-net-how-to-customize-messagebox
                MessageBox.Show($"Unable to parse input file.\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Person person = new Person(intWaitTime, intTellerTime);
            return person;
        }

        private void processArrival(Person p) {
            Console.WriteLine(p.arrivalTime + " " + p.windowTime);
            queue.pushQueue(p); //  Add new customer to tail of queue
            if (queue.count() == 1) { // If queue was empty
                // TODO: Create departure event
                // TODO: Make model of queue on GUI
            }
        }
    }
}

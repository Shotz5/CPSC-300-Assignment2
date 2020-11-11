using System;
using System.IO;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {
        public OpenFileDialog openFile;
        public Queue queue;
        public EventList eventList;
        public int time;
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
                    Event arrivalEvent = new Event(Event.ARRIVAL, (person.arrivalTime + person.windowTime)); // Fix this
                    processArrival(person);
                }
            }

            // Fix this too, not ordered properly tbh
            while(!eventList.isEmpty()) {
                Event eve = (Event) eventList.Dequeue();
                if (eve.type == Event.ARRIVAL) {

                } else if (eve.type == Event.DEPARTURE) {
                    processDeparture();
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
                MessageBox.Show($"Unable to parse input file.\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Person person = new Person(intWaitTime, intTellerTime);
            return person;
        }

        // Meh not right
        private void processArrival(Person p) {
            queue.pushQueue(p); //  Add new customer to tail of queue
            if (queue.Count() == 1) { // If queue was empty
                Event newDeparture = new Event(Event.DEPARTURE, (p.arrivalTime + p.windowTime)); // Create departure event
                eventList.Enqueue(newDeparture);
            } else {

            }
        }

        // Also not quite right
        private void processDeparture() {
            Person p = (Person) queue.popQueue();
            if (!queue.isEmpty()) {
                Event newDeparture = new Event(Event.DEPARTURE, (p.arrivalTime + p.windowTime));
                eventList.Enqueue(newDeparture);
            }
        }
    }
}

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {

        public OpenFileDialog openFile;
        private StreamReader fileInput;

        public Queue customerQueue;
        public EventList eventList;
        public int time;
        public int timeWaiting;

        public MainWindow() {
            InitializeComponent();
            customerQueue = new Queue();
            eventList = new EventList();
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
                    fileInput = new StreamReader(openFile.FileName);
                } catch (Exception ex) {
                    MessageBox.Show($"{ex.Message}\n\n {ex.StackTrace}");
                }
            } else if (result == DialogResult.Cancel) {
                MessageBox.Show("User cancelled Bank Line input");
                return;
            }

            var button = (Button) sender;
            button.Visible = false;

            Event firstArrival = readNextArrival(); // Read first arrival event from data file
            eventList.Enqueue(firstArrival); // and put it in the event list

            while(!eventList.isEmpty()) { // while eventlist is not empty
                Event nextEvent = eventList.Dequeue(); // take next event from the ordered event list
                if (nextEvent.getType() == Event.ARRIVAL) { // If it's an arrival event
                    processArrival(nextEvent); // Process arrival
                } else if (nextEvent.getType() == Event.DEPARTURE) { // If it's a departure event
                    processDeparture(); // Process departure
                }
            }
        }
        private Event readNextArrival() {
            String nextLine = fileInput.ReadLine();
            if (nextLine == null) {
                return null;
            }
            Person person = parsePerson(nextLine);
            Event arrivalEvent = new Event(Event.ARRIVAL, person.getArrivalTime(), person);
            return arrivalEvent;
        }

        // ARRIVAL TIMES WORK, THIS DEPARTURE TIME LOGICALLY SHOULD WORK?
        private void processArrival(Event eve) {
            Person eventPerson = eve.getPerson(); // Get person for event
            customerQueue.pushQueue(eventPerson); // Add new customer to tail of queue
            time = eventPerson.getArrivalTime();

            Console.WriteLine("Person " + eventPerson.getPersonNumber() + " arrived at " + time); // Customer has arrived

            if (customerQueue.Count() == 1) { // Customer queue was empty
                Event departureEvent = new Event(Event.DEPARTURE, (eventPerson.getArrivalTime() + eventPerson.getWindowTime()), eventPerson); // Create new departureEvent
                eventList.Enqueue(departureEvent); // Insert this in order in the eventList
                timeWaiting += 0;
            }

            Event nextArrival = readNextArrival();
            if (nextArrival != null) { // If not EOF
                eventList.Enqueue(nextArrival); // Read next arrival and ordered insert it
            }
        }

        // THESE TIME STAMPS DO NOT WORK, WHY CANT I GET IT TO WORK AHHHHHHHH
        // THIS IS GARBAGE FIX THIS
        private void processDeparture() {
            Person finishedCustomer = customerQueue.popQueue();
            time = (timeWaiting + finishedCustomer.getArrivalTime() + finishedCustomer.getWindowTime());

            Console.WriteLine("Person " + finishedCustomer.getPersonNumber() + " popped at " + time);

            if (customerQueue.Count() != 0) {
                Person nextPerson = customerQueue.peek();
                Event departureEvent = new Event(Event.DEPARTURE, (time + nextPerson.getWindowTime()), nextPerson);
                eventList.Enqueue(departureEvent);
                timeWaiting += (time - nextPerson.getArrivalTime());
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

        private void MainWindow_Load(object sender, EventArgs e) {

        }
    }
}

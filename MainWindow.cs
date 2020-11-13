using System;
using System.IO;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {

        
        private StreamReader fileInput;

        public Queue customerQueue;
        public EventList eventList;
        public int time;

        public MainWindow() {
            InitializeComponent();
            // Initialize empty queue and eventlist
            customerQueue = new Queue();
            eventList = new EventList();
        }

        /// <summary>
        /// When user clicks the input file button
        /// </summary>
        /// <returns>void</returns>
        private void inputfile_Click(object sender, EventArgs e) {
            // Sets default parameters for Windows file section box
            OpenFileDialog openFile = new OpenFileDialog() {
                FileName = "BankFile.txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open Bank Lineup File"
            };

            // Show the diaglog to the user
            DialogResult result = openFile.ShowDialog();
            // If user selects file and clicks 'ok'
            if (result == DialogResult.OK) {
                try {
                    fileInput = new StreamReader(openFile.FileName);
                } catch (Exception ex) {
                    MessageBox.Show($"{ex.Message}\n\n {ex.StackTrace}");
                }
            // If user cancels input
            } else if (result == DialogResult.Cancel) {
                MessageBox.Show("User cancelled Bank Line input");
                return;
            }

            // If error parsing, stop
            if (!checkOrder()) {
                return;
            }
            fileInput = new StreamReader(openFile.FileName);
            Person.resetPersonCounter();

            // Hide input file selection button
            var button = (Button) sender;
            button.Visible = false;

            // Read first arrival event from data file
            Event firstArrival = readNextArrival();
            // And put it in the event list
            eventList.Enqueue(firstArrival);

            // while eventlist is not empty
            while (!eventList.isEmpty()) {
                // take next event from the ordered event list
                Event nextEvent = eventList.Dequeue();
                // If it's an arrival event
                if (nextEvent.getType() == Event.ARRIVAL) {
                    // Process arrival
                    processArrival(nextEvent);
                // If it's a departure event
                } else if (nextEvent.getType() == Event.DEPARTURE) {
                    // Process departure
                    processDeparture(); 
                }
            }
            this.textBox1.Visible = true;
        }


        /// <summary>
        /// Reads the next line from the file and returns an arrival event
        /// </summary>
        /// <returns>Event of type Arrival</returns>
        private Event readNextArrival() {
            String nextLine = fileInput.ReadLine();
            if (nextLine == null) {
                return null;
            }
            Person person = parsePerson(nextLine);
            Event arrivalEvent = new Event(Event.ARRIVAL, person.getArrivalTime(), person);
            return arrivalEvent;
        }

        /// <summary>
        /// Processes an arrival event
        /// </summary>
        /// <returns>void</returns>
        private void processArrival(Event eve) {
            // Get person for event
            Person eventPerson = eve.getPerson();
            // Add new customer to tail of queue
            customerQueue.pushQueue(eventPerson); 
            // Update current time
            time = eventPerson.getArrivalTime();

            this.textBox1.AppendText(String.Format("Time:{0,6}    Person Number:{1,6}    Arrived", time, eventPerson.getPersonNumber())); // Customer has arrived
            this.textBox1.AppendText(Environment.NewLine);

            // Customer queue was empty
            if (customerQueue.Count() == 1) {
                // Create new departureEvent
                Event departureEvent = new Event(Event.DEPARTURE, (eventPerson.getArrivalTime() + eventPerson.getWindowTime()), eventPerson);
                // Insert this in order in the eventList
                eventList.Enqueue(departureEvent); 
                // Update wait time
                eventPerson.setWaitTime(0);
            }

            Event nextArrival = readNextArrival();
            // If not EOF
            if (nextArrival != null) {
                // Read next arrival and ordered insert it
                eventList.Enqueue(nextArrival); 
            }
        }


        /// <summary>
        /// Processes a departure event
        /// </summary>
        /// <returns>void</returns>
        private void processDeparture() {
            // Remove first customer from queue
            Person finishedCustomer = customerQueue.popQueue();
            // Update current time
            time = (finishedCustomer.getArrivalTime()  + finishedCustomer.getWaitTime() + finishedCustomer.getWindowTime());

            this.textBox1.AppendText(String.Format("Time:{0,6}    Person Number:{1,6}    Departed   Waited: {2,3}", time, finishedCustomer.getPersonNumber(), finishedCustomer.getWaitTime()));
            this.textBox1.AppendText(Environment.NewLine);

            // If someone else is waiting
            if (customerQueue.Count() != 0) {
                // Create a departure event for that person
                Person nextPerson = customerQueue.peek();
                Event departureEvent = new Event(Event.DEPARTURE, (time + nextPerson.getWindowTime()), nextPerson);
                // Ordered insert into event list
                eventList.Enqueue(departureEvent);
                // Update time spent waiting
                nextPerson.setWaitTime(time - nextPerson.getArrivalTime());
            }
        }

        /// <summary>
        /// Parses the input string into a object of type person
        /// </summary>
        /// <returns>Person</returns>
        private Person parsePerson(String input) {
            // Format is (x,y) x - arrival time / y - window time
            string arrivalTime = "";
            string windowTime = "";
            bool waitBool = true;
            // For every character in the string, pull out the numbers, when you encounter a comma, start writing to windowTime
            for (int i = 0; i < input.Length; i++) {
                if (Char.IsDigit(input[i]) && waitBool) {
                    arrivalTime += input[i];
                } else if (input[i] == ',') {
                    waitBool = false;
                } else if (Char.IsDigit(input[i]) && !waitBool) {
                    windowTime += input[i];
                }
            }
            int intArrivalTime = -1;
            int intWindowTime = -1;
            try {
                intArrivalTime = Int32.Parse(arrivalTime);
                intWindowTime = Int32.Parse(windowTime);
            } catch (Exception e) {
                MessageBox.Show($"Unable to parse input file at {input}.\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            Person person = new Person(intArrivalTime, intWindowTime);
            return person;
        }

        private bool checkOrder() {
            string nextLine;
            int lastArrivalTime = 0;

            while ((nextLine = fileInput.ReadLine()) != null) {
                Person p = parsePerson(nextLine);
                
                if (p == null) {
                    return false;
                }

                int thisArrivalTime = p.getArrivalTime();
                int thisWindowTime = p.getWindowTime();

                // If arrivalTimes aren't in order, or windowTime is negative
                if (lastArrivalTime > thisArrivalTime || thisWindowTime < 1) {
                    MessageBox.Show($"Unable to parse input file at {nextLine}.\n\n Times are not in numerical order or time at window is invalid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void outputfile_Click(object sender, EventArgs e) {
            string path;
            FolderBrowserDialog selectFolder = new FolderBrowserDialog() {
                Description = "Select folder"
            };

            DialogResult result = selectFolder.ShowDialog();

            if (DialogResult == DialogResult.OK) {
                path = selectFolder.SelectedPath;
            }
            
            //File.WriteAllLines(path, )
        }
    }
}

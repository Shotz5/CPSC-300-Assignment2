using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {
        
        private StreamReader fileInput;
        private ArrayList peopleServed;
        private Queue customerQueue;
        private EventList eventList;
        private int time;

        private string writeVar;
        private int count = 0;
        private int totalCount = 0;

        /// <summary>
        /// Initialize the main application window
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            // Initialize empty queue and eventlist
            customerQueue = new Queue();
            eventList = new EventList();
            peopleServed = new ArrayList();
            writeVar = "";
        }

        /// <summary>
        /// EventHandler for input file button click
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

            // Initial check to make sure all arrival times are ordered and are parsable
            if (!checkFile()) {
                return;
            }

            // Reset Stream from top of file by just resetting the stream
            fileInput = new StreamReader(openFile.FileName);

            // Hide input file selection button
            var button = (Button) sender;
            button.Visible = false;

            // Show progress bar
            progress.Visible = true;
            progress.Maximum = (totalCount * 3);
            simulating.Visible = true;

            // Read first arrival event from data file
            Event firstArrival = readNextArrival();

            // And put it in the event list
            eventList.Enqueue(firstArrival);

            // Run Main execution async from GUI thread
            backgroundWorker1.RunWorkerAsync();
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

            // Write to variable
            writeVar += String.Format("Time:{0,6}    Person Number:{1,6}    Arrived", time, eventPerson.getPersonNumber());
            writeVar += Environment.NewLine;
            count++;

            // Customer queue was empty
            if (customerQueue.Count() == 1) {
                // Create new departureEvent
                Event departureEvent = new Event(Event.DEPARTURE, (eventPerson.getArrivalTime() + eventPerson.getWindowTime()), eventPerson);

                // Insert this in order in the eventList
                eventList.Enqueue(departureEvent); 

                // Update persons wait time
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

            // Add it to list for final summary
            peopleServed.Add(finishedCustomer);

            // Update current time
            time = (finishedCustomer.getDepartureTime());

            // Write to variable
            writeVar += String.Format("Time:{0,6}    Person Number:{1,6}    Departed   Waited: {2,3}", time, finishedCustomer.getPersonNumber(), finishedCustomer.getWaitTime());
            writeVar += Environment.NewLine;
            count++;

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
            
            // Parse the parsed strings into ints
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

        /// <summary>
        /// Checks file for invalid data before running through it
        /// </summary>
        /// <returns>boolean true = continue / false = stop</returns>
        private bool checkFile() {
            string nextLine;
            int lastArrivalTime = 0;

            // While the file has new input
            while ((nextLine = fileInput.ReadLine()) != null) {
                Person p = parsePerson(nextLine);
                totalCount++;
                
                // If there was an error parsing a person
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
                lastArrivalTime = thisArrivalTime;
            }

            // Reset person counter back to 1
            Person.resetPersonCounter();
            return true;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            // While eventlist is not empty
            while (!eventList.isEmpty()) {
                // Invoke control of the GUI if 1000 lines have been written
                if (count == 1000) {
                    updateGUI();
                }

                // Take next event from the ordered event list
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

            // Write rest of writeVar to textbox
            updateGUI();

            generateFinalSummary();
        }

        private void updateGUI() {
            backgroundWorker1.ReportProgress(count);
            this.Invoke(new MethodInvoker(delegate () {
                textBox1.AppendText(writeVar);
            }));
            writeVar = "";
            count = 0;
        }

        private void updateGUI(int i) {
            backgroundWorker1.ReportProgress(i);
            this.Invoke(new MethodInvoker(delegate () {
                textBox1.AppendText(writeVar);
            }));
            writeVar = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
            progress.Value += e.ProgressPercentage;

            double processingReport = (double) progress.Maximum * (2d / 3d);
            int intProcessingReport = (int) processingReport;

            if (intProcessingReport == progress.Value) {
                simulating.Visible = false;
                finalsummary.Visible = true;
            }

            if (progress.Value == progress.Maximum) {
                textBox1.Visible = true;
                outputfile.Visible = true;
                finalsummary.Visible = false;
                progress.Visible = false;
            }
        }

        /// <summary>
        /// Generate final summary for customers visiting in simulation
        /// </summary>
        private void generateFinalSummary() {
            writeVar += Environment.NewLine;
            writeVar += "All events complete. Final summary: ";
            writeVar += Environment.NewLine;
            writeVar += Environment.NewLine;
            writeVar += "Customer Number | Arrival Time | Wait Time | Arrival At Window | Window Time | Departed At";
            writeVar += Environment.NewLine;
            writeVar += "------------------------------------------------------------------------------------------";
            writeVar += Environment.NewLine;

            // Iterates through all visited customers
            for (int i = 0; i < peopleServed.Count; i++) {
                Person person = (Person) peopleServed[i];
                writeVar += String.Format("{0,15} | {1,12} | {2,9} | {3,17} | {4,11} | {5,11}",
                    person.getPersonNumber(),
                    person.getArrivalTime(),
                    person.getWaitTime(),
                    person.getArrivalAtWindow(),
                    person.getWindowTime(),
                    person.getDepartureTime()
                    );
                writeVar +=Environment.NewLine;

                // When 1000 lines have been generated, invoke UI thread and append text
                if ((i % 1000) == 0) {
                    updateGUI(1000);
                }
            }

            // Append rest to gui
            updateGUI();
        }

        /// <summary>
        /// Outputs text in textbox to .txt file
        /// </summary>
        private void outputfile_Click(object sender, EventArgs e) {
            string path = "";

            // Builds user dialog
            OpenFileDialog selectFolder = new OpenFileDialog() {
                FileName = "output.txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Select where to output bank lineup file",
                CheckFileExists = false
            };

            // Shows user dialog box
            DialogResult result = selectFolder.ShowDialog();

            // If user clicks 'open'
            if (result == DialogResult.OK) {
                path = selectFolder.FileName;
            // If user clicks 'cancel'
            } else if (result == DialogResult.Cancel) {
                MessageBox.Show($"User cancelled file output.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            File.WriteAllText(path, textBox1.Text);
        }
    }
}

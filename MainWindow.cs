using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {
        
        private StreamReader fileInput;
        private LinkedList<Person> peopleServed;
        private Queue customerQueue;
        private EventList eventList;
        private int time;

        StringBuilder writeVar;
        private int totalCount;
        private int progressBarUpdate;
        private int counter;

        /// <summary>
        /// Initialize the main application window
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            // Initialize empty queue and eventlist, and other vars needed
            customerQueue = new Queue();
            eventList = new EventList();
            peopleServed = new LinkedList<Person>();
            writeVar = new StringBuilder();
            totalCount = 0;
            progressBarUpdate = 0;
            counter = 0;
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
            progressBarUpdate = (int) ((totalCount * 3) * ((double)(1d / 100d)));
            simulating.Visible = true;

            // Read first arrival event from data file
            Event firstArrival = readNextArrival();

            // And put it in the event list
            eventList.Enqueue(firstArrival);

            // Run Main execution async from GUI thread
            backgroundWorker1.RunWorkerAsync();
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

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            // While eventlist is not empty
            while (!eventList.isEmpty()) {
                if (counter % progressBarUpdate == 0) {
                    backgroundWorker1.ReportProgress(counter);
                    //appendToSimBox(counter);
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
                counter++;
            }

            // Simulation done
            appendToSimBox((int) ((double) progress.Maximum * (2d/3d)));
            // Write rest of writeVar to textbox
            generateFinalSummary();
            // Append the rest to the sum box
            appendToSumBox(totalCount * 3);
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

            try {
                // Write to variable
                writeVar.Append(String.Format("Time:{0,6}    Person Number:{1,6}    Arrived", time, eventPerson.getPersonNumber()));
                writeVar.Append(Environment.NewLine);
            } catch (Exception e) {
                MessageBox.Show($"Error {e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

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
            peopleServed.AddLast(finishedCustomer);

            // Update current time
            time = (finishedCustomer.getDepartureTime());

            try {
                // Write to variable
                writeVar.Append(String.Format("Time:{0,6}    Person Number:{1,6}    Departed   Waited: {2,3}", time, finishedCustomer.getPersonNumber(), finishedCustomer.getWaitTime()));
                writeVar.Append(Environment.NewLine);
            } catch (Exception e) {
                MessageBox.Show($"Error {e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

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

        private void appendToSimBox(int progress) {
            this.Invoke(new MethodInvoker(delegate {
                try {
                    this.simoutput.AppendText(writeVar.ToString());
                } catch (OutOfMemoryException e) {
                    MessageBox.Show($"Out of memory exception thrown! Too many people placed in line\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }}));
            writeVar.Clear();
            backgroundWorker1.ReportProgress(progress);
        }

        private void appendToSumBox(int progress) {
            this.Invoke(new MethodInvoker(delegate {
                try {
                    this.finalsumm.AppendText(writeVar.ToString());
                } catch (OutOfMemoryException e) {
                    MessageBox.Show($"Out of memory exception thrown! Too many people placed in line\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
            writeVar.Clear();
            backgroundWorker1.ReportProgress(progress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
            progress.Value = e.ProgressPercentage;

            int processingReport = (int)((double) progress.Maximum * (2d / 3d));

            // Done simulation
            if (processingReport == progress.Value) {
                simulating.Visible = false;
                finalsummary.Visible = true;
            }

            // Done finalsummary
            if (progress.Value == progress.Maximum) {
                simoutput.Visible = true;
                outputfile.Visible = true;
                finalsummary.Visible = false;
                progress.Visible = false;
                finalsumm.Visible = true;
                simout.Visible = true;
                sumout.Visible = true;
            }
        }

        /// <summary>
        /// Generate final summary for customers visiting in simulation
        /// </summary>
        private void generateFinalSummary() {
            try {
                writeVar.Append("All events complete. Final summary: ");
                writeVar.Append(Environment.NewLine);
                writeVar.Append(Environment.NewLine);
                writeVar.Append("Customer Number | Arrival Time | Wait Time | Arrival At Window | Window Time | Departed At");
                writeVar.Append(Environment.NewLine);
                writeVar.Append("------------------------------------------------------------------------------------------");
                writeVar.Append(Environment.NewLine);

                // Iterates through all visited customers
                while (peopleServed.Count > 0) {
                    if (counter % progressBarUpdate == 0) {
                        backgroundWorker1.ReportProgress(counter);
                        //appendToSumBox(counter);
                    }
                    Person person = peopleServed.First.Value;
                    writeVar.Append(String.Format("{0,15} | {1,12} | {2,9} | {3,17} | {4,11} | {5,11}",
                        person.getPersonNumber(),
                        person.getArrivalTime(),
                        person.getWaitTime(),
                        person.getArrivalAtWindow(),
                        person.getWindowTime(),
                        person.getDepartureTime()
                        ));
                    writeVar.Append(Environment.NewLine);
                    peopleServed.RemoveFirst();
                    counter++;
                }
            } catch (Exception e) {
                MessageBox.Show($"Error {e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Outputs text in textbox to .txt file
        /// </summary>
        private void outputfile_Click(object sender, EventArgs e) {
            string path = "";

            // Builds user dialog
            SaveFileDialog selectFolder = new SaveFileDialog() {
                FileName = "output.txt",
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Select where to output bank lineup file",
                CheckFileExists = false,
                CreatePrompt = true
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
            File.WriteAllText(path, simoutput.Text + finalsumm.Text);
        }
    }
}

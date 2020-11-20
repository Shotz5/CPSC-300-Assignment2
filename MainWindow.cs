/// Assignment 2 Main Window for User Interaction

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Assignment_2 {
    public partial class MainWindow : Form {
        
        private StreamReader FileInput;
        private LinkedList<Person> PeopleServed;
        private Queue CustomerQueue;
        private EventList EventList;
        private int Time;

        StringBuilder TextOutput;
        private int MaxPeople;
        private int ProgressBarUpdate;
        private int ProgressCounter;

        /// <summary>
        /// Initialize the main application window and all the variables
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            // Initialize empty queue and eventlist, and other vars needed
            CustomerQueue = new Queue();
            EventList = new EventList();
            PeopleServed = new LinkedList<Person>();
            TextOutput = new StringBuilder();
            MaxPeople = 0;
            ProgressBarUpdate = 0;
            ProgressCounter = 0;
        }

        /// <summary>
        /// EventHandler for input file button click
        /// </summary>
        /// <returns>void</returns>
        private void InputButton_Click(object sender, EventArgs e) {
            // Sets default parameters for Windows file section box
            OpenFileDialog openFile = new OpenFileDialog() {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                FileName = "BankFile.txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open Bank Lineup File"
            };

            // Show the diaglog to the user
            DialogResult result = openFile.ShowDialog();

            // If user selects file and clicks 'ok'
            if (result == DialogResult.OK) {
                try {
                    
                    // Run CheckFile
                    if (!CheckFile(openFile.FileName)) {
                        // Reset UI Changes
                        CheckLabel.Visible = false;
                        InputButton.Visible = true;
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label4.Visible = true;
                        label5.Visible = true;
                        return;
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"{ex.Message}\n\n {ex.StackTrace}");
                }
            // If user cancels input
            } else if (result == DialogResult.Cancel) {
                MessageBox.Show("User cancelled Bank Line input");
                return;
            }

            // Hide "Checking..." Label
            CheckLabel.Visible = false;

            // Show progress bar and set progrss bar values
            ProgressBar.Visible = true;
            ProgressBar.Maximum = (MaxPeople * 3);
            ProgressBarUpdate = (int) ((MaxPeople * 3) * ((double)(1d / 100d)));
            simulating.Visible = true;

            // Read first arrival event from data file
            Event firstArrival = ReadNextArrival();

            // And put it in the event list
            EventList.Enqueue(firstArrival);

            // Run Main execution async from GUI thread, goes to ExecutionThread.DoWork()
            ExecutionThread.RunWorkerAsync();
        }

        /// <summary>
        /// Checks file for invalid data before running through it
        /// </summary>
        /// <returns>boolean true = continue / false = stop</returns>
        private bool CheckFile(string filePath) {
            CheckLabel.Visible = true;
            InputButton.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;

            // The only possible file you can bring in is a .txt file, but this is here as a just-in-case
            if (Path.GetExtension(filePath) != ".txt") {
                MessageBox.Show("Unable to parse input file.\n\nFile is not a .txt file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FileInput = new StreamReader(filePath);
            string nextLine;
            int lastArrivalTime = 0;
            int lineCount = 0;

            // While the file has new input
            while ((nextLine = FileInput.ReadLine()) != null) {
                lineCount++;
                Person p = ParsePerson(nextLine);
                MaxPeople++;

                // If there was an error parsing a person
                if (p == null) {
                    FileInput.Close();
                    return false;
                }

                int thisArrivalTime = p.GetArrivalTime();
                int thisWindowTime = p.GetWindowTime();

                // If arrivalTimes aren't in order, or windowTime is negative
                if (lastArrivalTime > thisArrivalTime || thisWindowTime < 1) {
                    MessageBox.Show($"Unable to parse input file at Line {lineCount} : {nextLine}\n\n Times are not in numerical order from smallest to largest or Time at window is invalid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FileInput.Close();
                    return false;
                }
                lastArrivalTime = thisArrivalTime;
            }

            // If file is empty
            if (lineCount == 0) {
                MessageBox.Show("Unable to parse input file.\n\nFile is empty.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileInput.Close();
                return false;
            }

            // If more than 1m people in file
            if ((lineCount) > 1000000) {
                MessageBox.Show("Too many people entered.\n\nLikely chance that system will hang" +
                    " or System.OutOfMemoryException will occur.\n\n" +
                    "Please make sure you enter 1,000,000 people or less to guarantee exectution.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileInput.Close();
                return false;
            }

            // Reset PersonCounter back to 1
            Person.ResetPersonCounter();

            // Reset file stream to beginning
            FileInput = new StreamReader(filePath);

            // File will execute
            return true;
        }

        /// <summary>
        /// Reads the next line from the file and returns an arrival event
        /// </summary>
        /// <returns>Event of type Arrival</returns>
        private Event ReadNextArrival() {
            // Read in next line from file
            String nextLine = FileInput.ReadLine();

            // If EOF
            if (nextLine == null) {
                return null;
            }

            // Parse the line into a person
            Person person = ParsePerson(nextLine);

            // And create the arrival event for that person
            Event arrivalEvent = new Event(Event.ARRIVAL, person.GetArrivalTime(), person);
            return arrivalEvent;
        }

        private void ExecutionThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            // While eventlist is not empty
            while (!EventList.IsEmpty()) {
                // Every 1/100th of the amount of lines to be generated (MaxPeople * 3), update the progress bar
                if (ProgressBarUpdate != 0 && ProgressCounter % ProgressBarUpdate == 0) {
                    ExecutionThread.ReportProgress(ProgressCounter);
                }

                // Take next event from the ordered event list
                Event nextEvent = EventList.Dequeue();

                // If it's an arrival event
                if (nextEvent.GetEventType() == Event.ARRIVAL) {
                    // Process arrival
                    ProcessArrival(nextEvent);
                    // If it's a departure event
                } else if (nextEvent.GetEventType() == Event.DEPARTURE) {
                    // Process departure
                    ProcessDeparture();
                }
                ProgressCounter++;
            }

            // Simulation done, write output to window and update progress bar
            AppendToSimBox((int) ((double) ProgressBar.Maximum * (2d/3d)));

            // Generate the final summary
            GenerateFinalSummary();

            // Append the final summary to window and update progress bar
            AppendToSumBox(MaxPeople * 3);
        }

        /// <summary>
        /// Processes an arrival event
        /// </summary>
        /// <returns>void</returns>
        private void ProcessArrival(Event eve) {
            // Extract person allocated to event
            Person eventPerson = eve.GetPerson();

            // Add new customer to tail of queue
            CustomerQueue.Enqueue(eventPerson); 

            // Update current Time
            Time = eventPerson.GetArrivalTime();

            try {
                // Write to variable to append later
                TextOutput.Append(String.Format("Time:{0,10}    Person Number:{1,7}    Arrived", Time, eventPerson.GetPersonNumber()));
                TextOutput.Append(Environment.NewLine);
            // In case memory exception occurs
            } catch (Exception e) {
                MessageBox.Show($"Error {e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            // If customer queue was empty
            if (CustomerQueue.Count() == 1) {
                // Create new departureEvent
                Event departureEvent = new Event(Event.DEPARTURE, (eventPerson.GetArrivalTime() + eventPerson.GetWindowTime()), eventPerson);

                // Insert this in order in the EventList
                EventList.Enqueue(departureEvent); 

                // Update persons wait Time
                eventPerson.SetWaitTime(0);
            }

            // Read next arrival
            Event nextArrival = ReadNextArrival();

            // If not EOF
            if (nextArrival != null) {
                // Read next arrival and ordered insert it
                EventList.Enqueue(nextArrival); 
            }
        }

        /// <summary>
        /// Processes a departure event
        /// </summary>
        /// <returns>void</returns>
        private void ProcessDeparture() {
            // Remove first customer from queue
            Person finishedCustomer = CustomerQueue.Dequeue();

            // Add said person to list for final summary generation
            PeopleServed.AddLast(finishedCustomer);

            // Update current Time
            Time = finishedCustomer.GetDepartureTime();

            try {
                // Write to variable to append later
                TextOutput.Append(String.Format("Time:{0,10}    Person Number:{1,7}    Departed  Waited: {2,6}", Time, finishedCustomer.GetPersonNumber(), finishedCustomer.GetWaitTime()));
                TextOutput.Append(Environment.NewLine);
            // In case memory exception occurs
            } catch (Exception e) {
                MessageBox.Show($"Error {e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            // If someone else is moving up to the window
            if (CustomerQueue.Count() != 0) {
                // Create a departure event for that person
                Person nextPerson = CustomerQueue.Peek();
                Event departureEvent = new Event(Event.DEPARTURE, (Time + nextPerson.GetWindowTime()), nextPerson);

                // Ordered insert into event list
                EventList.Enqueue(departureEvent);

                // Update Time spent waiting
                nextPerson.SetWaitTime(Time - nextPerson.GetArrivalTime());
            }
        }

        /// <summary>
        /// Parses the input string into a object of type person
        /// </summary>
        /// <returns>Person</returns>
        private Person ParsePerson(String input) {
            // Format is (x,y) x - arrival Time / y - window Time
            string arrivalTime = "";
            string windowTime = "";

            // For every character in the string, write the numbers to arrivalTime, when you encounter a comma, start writing to windowTime
            int i = 0;
            while(i != input.Length && input[i] != ',') {
                if (Char.IsDigit(input[i]) || input[i] == '-') {
                    arrivalTime += input[i];
                }
                i++;
            }
            while(i != input.Length && input[i] != ')') {
                if (Char.IsDigit(input[i]) || input[i] == '-') {
                    windowTime += input[i];
                }
                i++;
            }

            // Dummy values that will cause an exception if not re-written
            int intArrivalTime = -1;
            int intWindowTime = -1;
            
            // Parse the parsed strings again into ints
            try {
                intArrivalTime = int.Parse(arrivalTime);
                intWindowTime = int.Parse(windowTime);
                if (intArrivalTime < 0 || intWindowTime < 1) {
                    MessageBox.Show($"Negative values entered at {input}.\n\nNo negative values can be entered, and Window Time must be greater than 1.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            } catch (Exception e) {
                MessageBox.Show($"Unable to parse input file at {input}.\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Build person from parsed times
            Person person = new Person(intArrivalTime, intWindowTime);
            return person;
        }

        /// <summary>
        /// Backgroundworker invokes control of UI thread and appends text to simulation output textbox
        /// </summary>
        /// <param name="progress"></param>
        private void AppendToSimBox(int progress) {
            string simoutput = TextOutput.ToString();
            // Invoke method sourced from https://stackoverflow.com/questions/12837305/cross-thread-operation-not-valid-how-to-access-winform-elements-from-another-mo
            this.Invoke(new MethodInvoker(delegate {
                try {
                    this.SimOutput.Text = simoutput;
                } catch (OutOfMemoryException e) {
                    MessageBox.Show($"Out of memory exception thrown! Too many people placed in line\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }));
            TextOutput.Clear();
            ExecutionThread.ReportProgress(progress);
        }

        /// <summary>
        /// Backgroundworker invokes control of UI thread and appends text to final summary output textbox
        /// </summary>
        /// <param name="progress"></param>
        private void AppendToSumBox(int progress) {
            string sumoutput = TextOutput.ToString();
            // Invoke method sourced from https://stackoverflow.com/questions/12837305/cross-thread-operation-not-valid-how-to-access-winform-elements-from-another-mo
            this.Invoke(new MethodInvoker(delegate {
                try {
                    this.FinalSummOutput.Text = sumoutput;
                } catch (OutOfMemoryException e) {
                    MessageBox.Show($"Out of memory exception thrown! Too many people placed in line\n\n{e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
            TextOutput.Clear();
            ExecutionThread.ReportProgress(progress);
        }

        /// <summary>
        /// Updates values based on progress reported by ReportProgress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecutionThread_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
            ProgressBar.Value = e.ProgressPercentage;

            // Value that represents (2/3's of people), done simulation at this value
            int processingReport = (int)((double) ProgressBar.Maximum * (2d / 3d));

            // Done simulation
            if (processingReport == ProgressBar.Value) {
                simulating.Visible = false;
                GenerateSummLabel.Visible = true;
            }

            // Done finalsummary
            if (ProgressBar.Value == ProgressBar.Maximum) {
                SimOutput.Visible = true;
                OutputButton.Visible = true;
                GenerateSummLabel.Visible = false;
                ProgressBar.Visible = false;
                FinalSummOutput.Visible = true;
                SimOutputLabel.Visible = true;
                FinalSumLabel.Visible = true;
            }
        }

        /// <summary>
        /// Generate final summary for customers visiting in simulation
        /// </summary>
        private void GenerateFinalSummary() {
            try {
                TextOutput.Append(Environment.NewLine);
                TextOutput.Append("All events complete. Final summary: ");
                TextOutput.Append(Environment.NewLine);
                TextOutput.Append(Environment.NewLine);
                TextOutput.Append("Customer Number | Arrival Time | Wait Time | Arrival At Window | Window Time | Departed At");
                TextOutput.Append(Environment.NewLine);
                TextOutput.Append("------------------------------------------------------------------------------------------");
                TextOutput.Append(Environment.NewLine);

                // Iterates through all visited customers
                while (PeopleServed.Count > 0) {
                    if (ProgressBarUpdate != 0 && ProgressCounter % ProgressBarUpdate == 0) {
                        ExecutionThread.ReportProgress(ProgressCounter);
                    }
                    Person person = PeopleServed.First.Value;
                    TextOutput.Append(String.Format("{0,15} | {1,12} | {2,9} | {3,17} | {4,11} | {5,11}",
                        person.GetPersonNumber(),
                        person.GetArrivalTime(),
                        person.GetWaitTime(),
                        person.GetArrivalAtWindow(),
                        person.GetWindowTime(),
                        person.GetDepartureTime()
                        ));
                    TextOutput.Append(Environment.NewLine);
                    PeopleServed.RemoveFirst();
                    ProgressCounter++;
                }
            // In case memory exception occurs
            } catch (Exception e) {
                MessageBox.Show($"Error {e.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Outputs text in both textboxes to .txt file
        /// </summary>
        private void OutputButton_Click(object sender, EventArgs e) {
            string path = "";

            // Builds user dialog
            SaveFileDialog selectFolder = new SaveFileDialog() {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
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
            File.WriteAllText(path, SimOutput.Text + FinalSummOutput.Text);
        }
    }
}

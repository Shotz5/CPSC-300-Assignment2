using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Assignment_2 {
    public partial class QueueDisplay : Form {

        Queue queue;
        EventList eventList;
        public QueueDisplay(string filePath) {
            InitializeComponent();
            queue = new Queue();
            eventList = new EventList();

            using(StreamReader stream = new StreamReader(filePath)) {
                string nextLine = "";
                while ((nextLine = stream.ReadLine()) != null) {
                    string nextPerson = stream.ReadLine();
                    Person person = parsePerson(nextPerson);
                    processArrival(person);
                }
            }
        }

        private Person parsePerson(String input) {
            // Format is (x,y) x - arrival time / y - teller time
            string[] parse = input.Split(',');
            int waitTime = -1;
            int tellerTime = -1;
            try {
                waitTime = Int32.Parse(parse[0]);
                tellerTime = Int32.Parse(parse[1]);
            } catch (Exception e) {
                // Sourced from https://stackoverflow.com/questions/2109441/how-to-show-error-warning-message-box-in-net-how-to-customize-messagebox
                MessageBox.Show("Unable to parse input file. Please ensure input is correct format!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            Person person = new Person(waitTime, tellerTime);
            return person;
        }

        private void processArrival(Person p) {
            queue.pushQueue()
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2 {
    /// <summary>
    /// Makes an event to be queued into EventList
    /// </summary>
    public class Event {
        public readonly static int ARRIVAL = 0;
        public readonly static int DEPARTURE = 1;

        private int type;
        private int time;
        private Person person;

        /// <summary>
        /// Initialize an Event
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <param name="person"></param>
        public Event(int type, int time, Person person) {
            this.type = type;
            this.time = time;
            this.person = person;
        }

        /// <summary>
        /// Get type of event
        /// </summary>
        /// <returns>Int - 0 - ARRIVAL / 1 - DEPARTURE</returns>
        public int getType() {
            return type;
        }

        /// <summary>
        /// Get person tied to the event
        /// </summary>
        /// <returns>Person</returns>
        public Person getPerson() {
            return person;
        }

        /// <summary>
        /// Gets time of event
        /// </summary>
        /// <returns>int time</returns>
        public int getTime() {
            return time;
        }
    }
}

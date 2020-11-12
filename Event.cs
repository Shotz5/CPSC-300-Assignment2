using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2 {
    public class Event {
        public static int ARRIVAL = 0;
        public static int DEPARTURE = 1;

        public int type;
        public int time;
        public Person person;
        public Event(int type, int time, Person person) {
            this.type = type;
            this.time = time;
            this.person = person;
        }

        public int getType() {
            return type;
        }

        public Person getPerson() {
            return person;
        }

        public int getTime() {
            return time;
        }
    }
}

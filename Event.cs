using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    public class Event {
        public static int ARRIVAL = 0;
        public static int DEPARTURE = 1;

        public int type;
        public int departureTime;
        public Event(int type, int departureTime) {
            this.type = type;
            this.departureTime = departureTime;
        }
    }
}

using System;
using System.Collections;
using System.Windows.Forms;

namespace Assignment_2 {
    public class EventList {
        private Queue eventList;
        public EventList() {
            eventList = new Queue();
        }

        public void Enqueue(Event ev) {
            eventList.pushQueue(ev);
        }

        public Event Dequeue() {
            return (Event) eventList.popQueue();
        }

        public bool isEmpty() {
            return eventList.isEmpty();
        }

        public int Count() {
            return eventList.Count();
        }
    }
}

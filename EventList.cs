using System;
using System.Collections;
using System.Windows.Forms;

namespace Assignment_2 {
    public class EventList {
        private ArrayList eventList;
        public EventList() {
            eventList = new ArrayList();
        }

        public void Enqueue(Event eve) {
            if (eventList.Count == 0) {
                eventList.Add(eve);
            } else if (eventList.Count == 1) {
                if (((Event)eventList[0]).getTime() < eve.getTime()) {
                    eventList.Insert(1, eve);
                } else {
                    eventList.Insert(0, eve);
                }
            } else {
                for (int i = 0; i < eventList.Count; i++) {
                    if (((Event)eventList[i]).getTime() > eve.getTime()) {
                        eventList.Insert(i, eve);
                        break;
                    }
                }
            }
        }

        public Event Dequeue() {
            Event eve = (Event) eventList[0];
            eventList.RemoveAt(0);
            return eve;
        }

        public bool isEmpty() {
            if (eventList.Count == 0) {
                return true;
            } else {
                return false;
            }
        }

        public int Count() {
            return eventList.Count;
        }
    }
}

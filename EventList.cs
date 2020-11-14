using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Assignment_2 {
    public class EventList {

        /// <summary>
        /// Builds ordered list of events
        /// </summary>
        private LinkedList<Event> eventList;

        /// <summary>
        /// Initializes an EventList
        /// </summary>
        public EventList() {
            eventList = new LinkedList<Event>();
        }

        /// <summary>
        /// <para>Numberically place an event in the queue</para>
        /// <para>Uses linear search because I can't be bothered to implement a better one right now</para>
        /// </summary>
        /// <param name="eve"></param>
        public void Enqueue(Event eve) {
            if (eventList.Count == 0) {
                eventList.AddFirst(eve);
            } else if (eventList.Count == 1) {
                if (((Event)eventList.First.Value).getTime() < eve.getTime()) {
                    eventList.AddLast(eve);
                } else {
                    eventList.AddFirst(eve);
                }
            } else {
                LinkedListNode<Event> nextEvent = eventList.First;
                while (nextEvent.Value.getTime() < eve.getTime()) {
                    nextEvent = nextEvent.Next;
                    if (nextEvent == null) {
                        eventList.AddLast(eve);
                        return;
                    }
                }
                eventList.AddAfter(nextEvent, eve);

                /*for (int i = 0; i < eventList.Count; i++) {
                    if (((Event)eventList[i]).getTime() > eve.getTime()) {
                        eventList.Insert(i, eve);
                        break;
                    }
                }*/
            }
        }

        /// <summary>
        /// Pop an item from the beginning of the queue
        /// </summary>
        /// <returns>Event</returns>
        public Event Dequeue() {
            Event eve = eventList.First.Value;
            eventList.RemoveFirst();
            return eve;
        }

        /// <summary>
		/// Checks if EventList is empty
		/// </summary>
		/// <returns>true - empty / false - not empty</returns>
        public bool isEmpty() {
            if (eventList.Count == 0) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
		/// Returns amount of items in EventList
		/// </summary>
		/// <returns>int - number of items in EventList</returns>
        public int Count() {
            return eventList.Count;
        }
    }
}

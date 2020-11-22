/// Assignment 2 EventList class for Event queue

using System;
using System.Collections.Generic;

namespace Assignment_2 {
    public class EventList {

        /// <summary>
        /// Builds ordered list of events
        /// </summary>
        private LinkedList<Event> ListEvents;

        /// <summary>
        /// Initializes an EventList
        /// </summary>
        public EventList() {
            ListEvents = new LinkedList<Event>();
        }

        /// <summary>
        /// <para>Numberically place an event in the queue</para>
        /// <para>Uses linear search because even with a million people, it still executes with relative efficiency</para>
        /// </summary>
        /// <param name="eve"></param>
        public void Enqueue(Event eve) {
            // If empty enqueue at 0
            if (ListEvents.Count == 0) {
                ListEvents.AddFirst(eve);
            // If list only has one value, solve with if
            } else if (ListEvents.Count == 1) {
                if (((Event)ListEvents.First.Value).GetTime() < eve.GetTime()) {
                    ListEvents.AddLast(eve);
                } else {
                    ListEvents.AddFirst(eve);
                }
            // Linear search insert
            } else {
                LinkedListNode<Event> nextEvent = ListEvents.First;
                while (nextEvent.Value.GetTime() < eve.GetTime()) {
                    nextEvent = nextEvent.Next;
                    if (nextEvent == null) {
                        ListEvents.AddLast(eve);
                        return;
                    }
                }
                ListEvents.AddAfter(nextEvent, eve);
            }
        }

        /// <summary>
        /// Pop an item from the beginning of the queue
        /// </summary>
        /// <returns>Event</returns>
        public Event Dequeue() {
            Event eve = ListEvents.First.Value;
            ListEvents.RemoveFirst();
            return eve;
        }

        /// <summary>
		/// Checks if EventList is empty
		/// </summary>
		/// <returns>true - empty / false - not empty</returns>
        public bool IsEmpty() {
            if (ListEvents.Count == 0) {
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
            return ListEvents.Count;
        }
    }
}

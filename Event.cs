/// Assignment 2 Event class for building an event

using System;

namespace Assignment_2 {
    /// <summary>
    /// Makes an event to be queued into EventList
    /// </summary>
    public class Event {
        public readonly static int ARRIVAL = 0;
        public readonly static int DEPARTURE = 1;

        private int EventType;
        private int EventTime;
        private Person EventPerson;

        /// <summary>
        /// Initialize an Event
        /// </summary>
        /// <param name="EventType"></param>
        /// <param name="EventTime"></param>
        /// <param name="EventPerson"></param>
        public Event(int EventType, int EventTime, Person EventPerson) {
            this.EventType = EventType;
            this.EventTime = EventTime;
            this.EventPerson = EventPerson;
        }

        /// <summary>
        /// Get EventType of event
        /// </summary>
        /// <returns>Int - 0 - ARRIVAL / 1 - DEPARTURE</returns>
        public int GetEventType() {
            return EventType;
        }

        /// <summary>
        /// Get EventPerson tied to the event
        /// </summary>
        /// <returns>Person</returns>
        public Person GetPerson() {
            return EventPerson;
        }

        /// <summary>
        /// Gets EventTime of event
        /// </summary>
        /// <returns>int EventTime</returns>
        public int GetTime() {
            return EventTime;
        }
    }
}

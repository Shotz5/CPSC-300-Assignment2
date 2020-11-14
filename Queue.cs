using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment_2 {

	/// <summary>
	/// FIFO Queue structure
	/// </summary>
	public class Queue {

		private static LinkedList<Person> queue;

		/// <summary>
		/// Initialize a queue
		/// </summary>
		public Queue() {
			queue = new LinkedList<Person>();
		}

		/// <summary>
		/// Remove an item from the beginning of the queue
		/// </summary>
		/// <returns>Person</returns>
		public Person popQueue() {
			Person p = queue.First.Value;
			queue.RemoveFirst();
			return p;
        }

		/// <summary>
		/// Pushes an item to the end of the queue
		/// </summary>
		/// <param name="p"></param>
		public void pushQueue(Person p) {
			queue.AddLast(p);
        }
		
		/// <summary>
		/// Checks if Queue is empty
		/// </summary>
		/// <returns>true - empty / false - not empty</returns>
		public bool isEmpty() {
			if (queue.Count == 0) {
				return true;
            } else {
				return false;
            }
        }

		/// <summary>
		/// Returns amount of items in queue
		/// </summary>
		/// <returns>int - number of items in queue</returns>
		public int Count() {
			return queue.Count;
        }

		/// <summary>
		/// Looks at what's on top of queue without popping
		/// </summary>
		/// <returns>Person</returns>
		public Person peek() {
			if (queue.Count != 0) {
				return queue.First.Value;
			} else {
				return null;
            }
        }
	}
}

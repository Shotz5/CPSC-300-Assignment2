using System;
using System.Collections;

namespace Assignment_2 {
	/// <summary>
	/// FIFO Queue structure
	/// </summary>
	public class Queue {

		private static ArrayList queue;

		/// <summary>
		/// Initialize a queue
		/// </summary>
		public Queue() {
			queue = new ArrayList();
		}

		/// <summary>
		/// Remove an item from the beginning of the queue
		/// </summary>
		/// <returns>Person</returns>
		public Person popQueue() {
			Person p = (Person) queue[0];
			queue.RemoveAt(0);
			return p;
        }

		/// <summary>
		/// Pushes an item to the end of the queue
		/// </summary>
		/// <param name="p"></param>
		public void pushQueue(Person p) {
			queue.Add(p);
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
				return (Person)queue[0];
			} else {
				return null;
            }
        }
	}
}

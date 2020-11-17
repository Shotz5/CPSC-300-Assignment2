/// Assignment 2 Queue class for person queues

using System;
using System.Collections.Generic;

namespace Assignment_2 {

	/// <summary>
	/// FIFO ListQueue structure
	/// </summary>
	public class Queue {

		private static LinkedList<Person> ListQueue;

		/// <summary>
		/// Initialize a ListQueue
		/// </summary>
		public Queue() {
			ListQueue = new LinkedList<Person>();
		}

		/// <summary>
		/// Remove an item from the beginning of the ListQueue
		/// </summary>
		/// <returns>Person</returns>
		public Person Dequeue() {
			Person p = ListQueue.First.Value;
			ListQueue.RemoveFirst();
			return p;
        }

		/// <summary>
		/// Pushes an item to the end of the ListQueue
		/// </summary>
		/// <param name="p"></param>
		public void Enqueue(Person p) {
			ListQueue.AddLast(p);
        }
		
		/// <summary>
		/// Checks if ListQueue is empty
		/// </summary>
		/// <returns>true - empty / false - not empty</returns>
		public bool IsEmpty() {
			if (ListQueue.Count == 0) {
				return true;
            } else {
				return false;
            }
        }

		/// <summary>
		/// Returns amount of items in ListQueue
		/// </summary>
		/// <returns>int - number of items in ListQueue</returns>
		public int Count() {
			return ListQueue.Count;
        }

		/// <summary>
		/// Looks at what's on top of ListQueue without popping
		/// </summary>
		/// <returns>Person</returns>
		public Person Peek() {
			if (ListQueue.Count != 0) {
				return ListQueue.First.Value;
			} else {
				return null;
            }
        }
	}
}

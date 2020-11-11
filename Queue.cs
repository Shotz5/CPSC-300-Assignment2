using System;
using System.Collections;

namespace Assignment_2 {
	public class Queue {

		public static ArrayList queue;

		public Queue() {
			queue = new ArrayList();
		}

		public Person popQueue() {
			Person p = (Person) queue[0];
			queue.RemoveAt(0);
			return p;
        }

		public void pushQueue(Person p) {
			queue.Add(p);
        }
		
		public bool isEmpty() {
			if (queue.Count == 0) {
				return true;
            } else {
				return false;
            }
        }

		public int count() {
			return queue.Count;
        }
	}
}

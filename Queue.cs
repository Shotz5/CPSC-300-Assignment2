using System;
using System.Collections;

namespace Assignment_2 {
	public class Queue {

		public static ArrayList queue;

		public Queue() {
			queue = new ArrayList();
		}

		public Object popQueue() {
			var p = queue[0];
			queue.RemoveAt(0);
			return p;
        }

		public void pushQueue(Object p) {
			queue.Add(p);
        }
		
		public bool isEmpty() {
			if (queue.Count == 0) {
				return true;
            } else {
				return false;
            }
        }

		public int Count() {
			return queue.Count;
        }
	}
}

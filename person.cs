using System;
using System.Windows.Forms;

namespace Assignment_2 {
	public class Person {

		private int arrivalTime;
		private int windowTime;
		private int waitTime;
		private static int people = 1;
		private int personNumber;
		public Person(int arrivalTime, int windowTime) {
			this.arrivalTime = arrivalTime;
			this.windowTime = windowTime;
			this.personNumber = people;
			people++;
		}

		public int getArrivalTime() {
			return arrivalTime;
        }

		public int getWindowTime() {
			return windowTime;
        }

		public int getPersonNumber() {
			return personNumber;
        }

		public void setWaitTime(int waitTime) {
			this.waitTime = waitTime;
        }

		public int getWaitTime() {
			return waitTime;
        }

		public int getArrivalAtWindow() {
			return (this.arrivalTime + this.waitTime);
        }

		public static void resetPersonCounter() {
			people = 1;
        }
	}
}
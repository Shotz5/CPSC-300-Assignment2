using System;
using System.Windows.Forms;

namespace Assignment_2 {
	/// <summary>
	/// Builds a person to be used in the customer queue
	/// </summary>
	public class Person {
		private static int people = 1;

		private int arrivalTime;
		private int windowTime;
		private int waitTime;
		private int personNumber;

		/// <summary>
		/// Initialize a person (this is a weird sentence)
		/// </summary>
		/// <param name="arrivalTime"></param>
		/// <param name="windowTime"></param>
		public Person(int arrivalTime, int windowTime) {
			this.arrivalTime = arrivalTime;
			this.windowTime = windowTime;
			this.personNumber = people;
			people++;
		}

		/// <summary>
		/// Get time person arrives at bank
		/// </summary>
		/// <returns>int arrivaltime</returns>
		public int getArrivalTime() {
			return arrivalTime;
        }

		/// <summary>
		/// Get time person will be at window
		/// </summary>
		/// <returns>int windowtime</returns>
		public int getWindowTime() {
			return windowTime;
        }

		/// <summary>
		/// Get person's auto-generated number
		/// </summary>
		/// <returns>int personnumber</returns>
		public int getPersonNumber() {
			return personNumber;
        }

		/// <summary>
		/// Set time person will have to wait to get to window
		/// </summary>
		/// <param name="waitTime"></param>
		public void setWaitTime(int waitTime) {
			this.waitTime = waitTime;
        }

		/// <summary>
		/// Get time person will have to wait to get to window
		/// </summary>
		/// <returns>int waittime</returns>
		public int getWaitTime() {
			return waitTime;
        }

		/// <summary>
		/// get time person will arrive at window
		/// </summary>
		/// <returns>int arrivaltime + waittime</returns>
		public int getArrivalAtWindow() {
			return (this.arrivalTime + this.waitTime);
        }

		/// <summary>
		/// Get time user will depart from bank
		/// </summary>
		/// <returns>int departuretime</returns>
		public int getDepartureTime() {
			return (this.arrivalTime + this.waitTime + this.windowTime);
        }

		/// <summary>
		/// Reset person counter to 1, needs to be reset because intitial check increments this value
		/// </summary>
		public static void resetPersonCounter() {
			people = 1;
        }
	}
}
/// Assignment 2 Person class to construct a Person

using System;

namespace Assignment_2 {
	/// <summary>
	/// Builds a person to be used in the customer queue
	/// </summary>
	public class Person {
		private static int People = 1;

		private int ArrivalTime;
		private int WindowTime;
		private int WaitTime;
		private int PersonNumber;

		/// <summary>
		/// Initialize a person (this is a weird sentence)
		/// </summary>
		/// <param name="arrivalTime"></param>
		/// <param name="windowTime"></param>
		public Person(int arrivalTime, int windowTime) {
			this.ArrivalTime = arrivalTime;
			this.WindowTime = windowTime;
			this.PersonNumber = People;
			People++;
		}

		/// <summary>
		/// Get time person arrives at bank
		/// </summary>
		/// <returns>int arrivaltime</returns>
		public int GetArrivalTime() {
			return ArrivalTime;
        }

		/// <summary>
		/// Get time person will be at window
		/// </summary>
		/// <returns>int windowtime</returns>
		public int GetWindowTime() {
			return WindowTime;
        }

		/// <summary>
		/// Get person's auto-generated number
		/// </summary>
		/// <returns>int personnumber</returns>
		public int GetPersonNumber() {
			return PersonNumber;
        }

		/// <summary>
		/// Set time person will have to wait to get to window
		/// </summary>
		/// <param name="waitTime"></param>
		public void SetWaitTime(int waitTime) {
			this.WaitTime = waitTime;
        }

		/// <summary>
		/// Get time person will have to wait to get to window
		/// </summary>
		/// <returns>int waittime</returns>
		public int GetWaitTime() {
			return WaitTime;
        }

		/// <summary>
		/// get time person will arrive at window
		/// </summary>
		/// <returns>int arrivaltime + waittime</returns>
		public int GetArrivalAtWindow() {
			return (this.ArrivalTime + this.WaitTime);
        }

		/// <summary>
		/// Get time user will depart from bank
		/// </summary>
		/// <returns>int departuretime</returns>
		public int GetDepartureTime() {
			return (this.ArrivalTime + this.WaitTime + this.WindowTime);
        }

		/// <summary>
		/// Reset person counter to 1, needs to be reset because intitial check increments this value
		/// </summary>
		public static void ResetPersonCounter() {
			People = 1;
        }
	}
}
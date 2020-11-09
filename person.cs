using System;

public class Person {

	public int accountNumber;
	public int queuePos;
	public int arrivalTime;
	public int windowTime;
	public Person(int accountNumber, int queuePos, int arrivalTime) {
		this.accountNumber = accountNumber;
		this.queuePos = queuePos;
		this.arrivalTime = arrivalTime;
	}
}

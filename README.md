# CPSC-300 Assignment 2 - **Bank Simulator**

This application takes as input a txt file of type ```(x,y)\n``` where x is the time the customer arrives, and y is the time the person takes at the window.

Upon input of a file the application begins an event-driven simulation where time is counted by filling a list with ```Arrival``` events and ```Departure``` events.

During these simulations, certain properties are appended to the ```Person``` such as ```arrivaltime```, ```waittime```, and ```departuretime```.

After the simulation has been completed a "final summary" is generated which lists attributes of every ```Person``` that was in the line.

Many catches exist to prevent both ```OutOfMemory Exceptions``` and invalid input. All of which are handled by the application, or are prevented from occuring in the first place.

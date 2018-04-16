# AKA-Salad_Chef_Simulation
1. Player Control : Press A,B,C,D,E OR F from keyboard to select a particular vegetable.

2. Score Criteria : 
a. if customer did not wait more than the expected waiting time then score will be -> Total score + (100 - Time taken by player to serve)
b. if customer is served wrong dish then score will be -> Total Score - 100(Penalty)
c. if customer waited more than the expected time but served right dish then score will be -> Total score - Extra Time wait(No extra points will be given )

3. Customers:
a. Served : if customer did not wait more than the expected waiting time.
b. Angry: if customer is not served with the right dish
c. dissatisfied: if customer is not served within the expected time interval

4. Remaining Tasks:
a.If a salad is given to a customer before 70% of the waiting time, the customer will award the player with a pickup. 
The pickup will be spawned at a random free spot in the level and can only be picked up by the player that satisfied the customer. • 
b. Implementation of different pickups:
o Speed: Increases the player’s movement speed for a period of time 
o Time: Increases the overall time that the player has left
o Score: Adds some points to the player score count 

This is a unity project I built that simulates what a game of quidditch might look like.
The rules of the game are simple, upon touching the ball, the team belonging to the player who touched the
ball is awarded one point, and the snitch is reset to a random position.

Snitch:
	The snitch has one parameter within the snitch behaviour script. In here, you can specify the degree of randomness
	in which the snitch will travel. larger numbers also mean for a faster snitch.

Players:
	The players themselves do not have attributes, however, they inherit their attrivutes from their team. Each player
	behaves autonomously of the other players, however it's behaviour is defined by the attributes given to that players team.
	Players bounce off players on their own team, and attempt to tackle players of the opposite team.  After being tackled, players
	become affected by gravity, and fall to the ground. upon hitting the ground, players respawn at their respective
	start position
	
Camera:
	The camera has some control, which is shown in the bottom right of the screen
	
	
Team Attributes:
	Max Velocity: The maximum speed a player can achieve;	
	Weight: Paramount to acceleration. The heavier an object is, the longer it takes to gain momentum
	Tackling probability: The probability of knocking an opponent off their broom after a collision.
	Accuracy: How likely a player is to steer towards the snitch. Essentially, how much a player will "overshoot" the snitch
	Aggression: How much force each player will exert on their own team after colliding. This is used to maintain space between players.


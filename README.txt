Streamline Studios Unity Test
Pablo Suarez

Controls:
WASD to roll the sphere
Mouse to move the camera around the sphere
Spacebar to jump (hold for jetpack)

Objective:
Collect all avocados in the scene to finish the level and
register your time (if it is within the best times)

What I'm most proud of would be the way I approached the sphere's
basic movement. The moment I read the requirements, I knew that
just making the sphere slide around wouldn't be enough, I wanted it
to actually roll around. This is a very particular challenge since
I'm trying to convert linear input (WASD/arrow keys) into rotations
in the sphere itself. I managed to remember about torque, and
used the Rigidbody's methods on adding a relative torque which would
naturally translate into the sphere's rotations. This, combined with
the camera movement guided by the mouse made this basic movement into
a surprisingly smooth locomotion system.

In my opinion, I can identify three places where I could've done better:
1. When I first planned the leaderboard functionality I wanted it to be
able to receive the initials of the player and save it. For the sake of
time, the scope was scaled down and this particular feature was cut, while
still making a functioning leaderboard
2. I wanted to add sound effects for every interaction in the game (the sphere
rolling, hitting the ground, the jetpack, collecting and avocado, finishing the
game, among others). Again, the scope was scaled back a bit in order to deliver
this test ASAP
3. I decided to make it so if the sphere is in the air, it can't be controlled,
as a way of embedding and interesting choice to the jetpack functionality (the
jetpack lets you reach all vertical spaces in the level, but you can't aim it
midair). I stand by this decision, but this makes the sphere feel floaty and
at times kind of frustrating to move around. I would've liked to iterate further
on this part

The time I spent on each part of this test (including this documentation)
can be found in:
https://docs.google.com/spreadsheets/d/1uWuJ0krF3tAEhtC8SuLo40RWh8700higgoA7OfeddyI/edit?usp=sharing
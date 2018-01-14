# 1942
Demo reel made in 20 hours based on 1942 game

# Code Explanation

### General Info
All the code has been written by me in the last week, except the main structure for the state machine. I copied the FSM from another of my projects and this is a simple version of few scripts created from scratch.
I try to use different implementations instead of using always the same methods, to show that I know how to use different ways of developing the same thing. This could be the reason why sometimes the code is weird. 
I think I comment more code than what is really necessary, because I use self explain vars, but I prefer to write extra comments better than missing any.
I dont like Unity Message, because the perfomance is not good and sometimes I am lost with what is happening, I prefer to make the code a little bit more dependent but knowing who I am calling.
I use 'inverse for' when the order of the 'for' is not important because it is a bit faster ( in small loops it is almost the same).
To allow the game pause , I decided to use a list to control the updates and allow to stop it. Using this I removed the unity Update ( which is not really good in perfomance). Other possible implementations are the use of events ( similar to the current implementation), set the timeStamp to 0 ( easiest solution but in my opinion bad because the loops are still happening) or to have some var in the manager ( LevelManager in my code ) and the class which can be pause check the value of this var to cancel the updates ( also bad solution in my opinion, too many checks )

It is missing in the code a few null checks with Warnings and Error messages. This is not done for lack of time, but I would add these kind of messages in the game.
The highScore is managed by the Game Manager, a better option would have been a 'save system' script which would be the responsable for saving and loading the data from playerpref.
Another possible improvement is use the "RequireComponent" class property
I decided also not to set the collision between planes, but it is something easy to add with a very similar system that now is using the bullet collision. 
Also it is not set the collision between bullets.
Also I didn't do a any Power Up
I decide to finish the level when all the enemys are spawn and deads. The idea was do some enemies that never go out the screen, so you have to killed or you never could win

### Spawn Enemy Info
I decided to use a script with the enemies spawn Info, setting the position and some vars of the enemies. The idea is to have a file with all this info and the game just loads it. I use this system instead of a enless or based in logic spawing because in this games ( at least the old ones ) uses this system . So if you know enough about the game, you can know exactly how many enemies there are and from where they are coming.

### Enemy
The enemy behavior is set by a FSM. Now I am have only a kind of AI and one kind of enemy. Add another enemy is simple, add some another behavior, maybe another kind of weapon, or another kind of bullet.

### World (WorldEntity)
World entity is who manages the world behavior and actions. It manages how fast the world moves down. Now this speed is set by the inspector, the idea is to create a level File with the level properties where the speed var will be specified.
World has a subentity called "WorldBackground", which manages the background elements and it is the responsable to show and make the movement itself. The idea is to set the backgrounds with the current level ( now it is set in 3 backgrounds ). 
Those backgrounds will have the level background set one on top of the one before, making a column. 
Everything will move down and when one goes out of the screen, this one will be reallocated on top and change the sprite to follow the level sequence

### Bullets
Now there are only two kinds of bullets, enemies and player, the only difference is the layer. The idea is to have different bullets with different behavior.
There is a class called 'bullet movement' which controls how the bullet moves. The idea is to extend this class and add different behavior to create different bullets, for example you can create a bullet with a wave movement.

### Weapons
Now there is only one kind of weapon. The idea is to extend the weapon class and add different weapons, for example a rotate weapon or a weapon which shoots forward and backwards at the same time. Also the way that the weapon is added ( now by the editor) should be changed to a json, csv, asset bundle or any other way.

### Entity Fire
This component allows one entity to shoot, the entity can have different weapons that will shoot.
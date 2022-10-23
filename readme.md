# Cat Burglar - No, Really
## Assignment 2 - Massey 159.361 by Joshua Pearson and Stephanie Shrimpton

## The game

![image](https://user-images.githubusercontent.com/36744690/197416839-da789887-9240-4b5a-aa55-c447337aa8f1.png)

- Play as a very well-trained cat, whose goal is to steal items from a supermarket for their owners after dark

## General Features

- Full working gameplay with tasks to complete, guards to avoid, a large supermarket to explore and items to steal.
- Dynamic and customisable inventory system
- Randomly generated task-based gameplay requiring certain items and actions to win a round
- Smart patrolling fully animated AI out to try and stop you.
- Lure AI with feline tactics such as meowing
- A full locally stored achievements system adding replayability
- Spooky music, creepy vibe and lighting and fog to match

## Contributions overview

#### Joshua Pearson
- General player mechanics, camera, climbing and movement system
- Custom built achievements system
- Abstract task system linking
- Item collection, inventory and item return systems
- In-game HUD setup and design

#### Stephanie Shrimpton
- Fully animated patrol guards and title screen cat
- Complete AI system with state management, linked with game mechanics
- Title screen and general UI design including scene management
- Mood, ambience, fog, lighting set up
- World and 3D space building

## Detailed contributions and game design

### Main design and idea
We knew we had to have a number of collectible items for the player to find. However, we also knew that it would be a bit tedious without some sort of stakes. So, we decided that we needed AI that would chase and catch the player, if they are spotted.

Because you play as a cat, we also wanted to implement more than one way of movement, to help the player feel more immersed. We also wanted to introduce a less one dimensional way of navigating around the AI, in the form of a ‘meow’ to distract them.

These were all important aspects of our game, but we had to start with the basics.  

### World Building 

#### AI VS Player walkable environment
First things first, everything had to be HUGE. To really sell the idea that you are playing as a cat, all objects in the environment had to be scaled up. We also had to decide how big we wanted our map to be. We implemented three rooms, with the option of a fourth, should (once our game is done) we feel it would be more balanced with another room.

Then, we created a maze-like scene of shopping shelves to introduce a challenge to the player when finding items. We wanted some items to be unreachable from the ground level, so we introduced a few ways of getting on top of the shelves. This also gave players a safe space to hide/escape to. Given the goal of the game, we wanted to give the player a bit of an edge in this way. 

### Player Movement/Mechanics 

#### Walking & Jumping
We kept the walking and jumping simple but still physics based. All movement is done with WASD and the camera and looking around is controlled with the mouse.  

#### Climbing
We made it so there were some places onto which a player could jump or walk up to that allowed climbing up. This could be used to get a vantage point over the guards, or to get to a few tricky to reach items. This was done with customisable layer tags to define the Climbable objects easily.

#### Meow
We added a meow mechanic which is manually controlled and useful for luring guards to a certain place. It also makes up a small percentage of the tasks and achievements discussed later. If a guard hears a meow (determined with a distance based spherecast) then it will be immediately interested in the location where the meow occurred.

### Artificial Intelligence 

#### General
To begin with the AI, we baked our nav mesh so that the guards would be able to walk around the floor of the supermarket. We had a placeholder cylinder AI in each room. Each AI cylinder had their own corresponding patrol points placed strategically in their respective rooms. 

#### Patrolling
For these guards, we had three different states.
In a patrol state, guards are set up to move to and from each respective patrol point, whilst also checking if the player is within their view every frame. Should the AI “spot” the player, it will move into a new chase state.

#### Chasing
When in a chase state, the guard will move to the last seen position of the player, at a higher speed than when patrolling. In fact, it is much faster than the player, and you need to be quick if you want to escape when spotted. If the guard reaches the last known position, does not catch and cannot see the player, it will move into a new search state.

#### Searching
Inside the search state, guards will check positions near the last seen point, but if more than 5 seconds has passed, they will return to patrolling. One thing we could improve upon here, would be fine-tuning where the guards will check within this state to avoid attempting to go to points that are too far away.

#### Distracted
We utilised a sphere cast for when the player meows, to determine if the AI is within earshot before setting the AI’s position to the player’s position at the time of the meow. Then, inside the search and patrol state, check when the AI has reached that position, to then enter a new search state.

#### Animation
Once we were done with the code, we imported the player mesh from the lab test as well as some animations and set it up so that the guards would walk whilst patrolling, and run when chasing the player.

### Game tasks

#### Inventory
A complete item inventory system was built and then displayed on the HUD with item space limitations. This gave us some functionality to create tasks and achievements based on item retrieval.

#### Collectable Items
We took some of the items from the supermarket shelves and started laying some of them around the room, in spots where cats might be able to sneakily grab them without it being noticed they were missing. That’s where you come in!
 
#### Tasks
A completely customisable interface was designed to make different task types and allow easy extension of the gameplay. After playing around with the most logical ones, we settled with item retrieval, and a few fun ones (like meow a number of times… cats love to meow)! There is a simple controller dedicated to managing the possible tasks which allows easy customisation and game design.

### Title/Loading Screen

#### Design
We implemented a loading screen, which preloads audio and any global non-singleton systems, like the achievements system. We also have an animated cat in the title screen, to make the scene feel more alive and less static. 

#### How to play
Although we tried to make the gameplay as intuitive as possible, we thought it would be a good idea to have a panel to explain to players what the game is all about, and how they should be playing it.

#### Achievements
To make sure that the game remained fun to play a few times, and because we both are achievement hunters too, we decided to add a local achievements system. This made it fun to replay while attempting to complete everything on the list.

### Sound/Lighting Design

#### Music:
We have atmospheric rain playing upon start up in the loading screen, and our title music comes in once the game has loaded. We thought it was a smooth transition and really added to the feel of the game from the get go. 

Inside the game itself, we wanted to switch it up and create a sense of stealth/urgency with the music, so we used a new track.

#### SFX:
We included sound effects for picking up items and winning the game. We also implemented a sound effect for when the player is spotted by the AI, to not only give the player a heads-up, but hopefully, get their hearts racing a little bit.

#### Lighting:
Initially, everything was well lit and clear for the player, but to go along with the uncertain, spooky feel, we decided to chuck some fog in and make it darker.

## Things that we wanted to implement and did not

#### Other tasks
We looked into possibly some other types of tasks, like survival time tasks, catch mice task, but they added extra complexity that made it well beyond the scope of this project. Also a survival time system encouraged the player to get up high or into a cubby hole and sit there and do nothing for a certain amount of time.

#### Adding footsteps to the patrolling guards
This was looked into and then scrapped. One of the main reasons was that it was going to be a big job. But actually the reason we opted not to do this in the end was due to the fact that it removed some of the "scariness" factor as a player. We wanted it to be that you couldn't hear the guards coming - which might not be super realistic since cats are incredibly good at hearing but it detracted from the gameplay. There was probably a world and way we could have put this into the game carefully without disrupting the player experience but we didn't go much deeper there.

## Known issues
#### The guards behaviour 
For some reason, the guards love to chase you into cubby holes and then sometimes continue waiting around for you to pop your head out. Sometimes they give up, if you get into the right position, they sort of forget you are there. This is not idea behaviour but the way the default Nav Mesh Agent works for setting destination meant that this was going to be a huge task to resolve more nicely. We didn't want to reduce difficulty by making it so that a guard will immediately stop chasing whe you entered a cubby so it was a hard mechanic to adjust.

#### Guards can see you on top of shelves
There were some ways to address this but in the end it was going to be quite difficulty to make this work well without making the game either much too easy or a lot harder. We ended up just making it such that if a guard spots you on top of shelves, they will come to investigate but give up quickly as they lose line of sight as they get close.

## Tech

Cat Burglar - No, Really was built using the following tools

- [Unity] - Unity 2021.3.10f1
- [Rider] - Rider 2022.3 EAP 4 by JetBrains
- [Visual Studio] - Visual Studio 2022
- [Github LFS] - Git Large File Storage system

## References

##### _Code_:
1. The editor play from scene script was adapted from code from [this link].

##### _Sounds and Music_
1. Title screen music was created by Josh.

##### _Images, models and visual assets_
1. Many 3D models and related assets of the supermarket, were sourced from the paid asset pack [Polygon Town Pack] (which was paid for by Josh).

   [Unity]: <https://unity.com/>
   [Github LFS]: <https://git-lfs.github.com/>
   [Rider]: <https://www.jetbrains.com/rider/>
   [Visual Studio]: <https://visualstudio.microsoft.com/vs/>
   [Polygon Town Pack]: <https://syntystore.com/products/polygon-town-pack>
   [this link]: <https://answers.unity.com/questions/441246/editor-script-to-make-play-always-jump-to-a-start.html>

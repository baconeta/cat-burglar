![image](https://user-images.githubusercontent.com/36744690/197417298-45014283-b237-4662-9df4-05f3f8ba8e94.png)
### A game designed and developed by Joshua Pearson and Stephanie Shrimpton

## The game
Play as a very well-trained cat, whose goal is to steal items from a supermarket for their owners after dark.

![image](https://user-images.githubusercontent.com/36744690/197416839-da789887-9240-4b5a-aa55-c447337aa8f1.png)

### A note to anyone playing this from the editor rather than building the game first
There is an editor script which should force any in-editor play to start from the correct first scene which is StartScene.
The edit menu should include a checkable box to enable or disable this feature. 

![image](https://user-images.githubusercontent.com/36744690/197418653-96da3fe1-a4b4-4fb7-9d84-fd3ce07708cd.png)

Note that the game will not work correctly if it is not played from the StartScene as it loads important singletons and static elements.
You will also not have the ability to adjust the in-game volume.

Editors note: if you play from a build, the mouse sensitivity may be automatically set to quite a bit higher than expected. 

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
The game was built with the cat burglar idea in mind quite early, with some form of task system implemented to encourage you to perform certain actions. This developed into an item collection/retrieval system to pair with the "we are doing things for our stupid humans at home" element.

Because you play as a cat, we also wanted to implement more than one way of movement, to help the player feel more immersed - the player can jump and climb around the world. We also wanted to introduce a not so one-dimensional way of navigating around the AI, in the form of a ‘meow’ to distract them.

### World Building 

#### AI vs Player walkable environment
To get started we knew everything had to be HUGE. To really sell the idea that you are playing as a cat, all objects in the environment are scaled up. We also had to decide how big we wanted our ground floor map to be. We implemented three large shelf-filled rooms.

![image](https://user-images.githubusercontent.com/36744690/197417152-1fdee2a1-562c-49e3-9f0c-7722da0287a9.png)

Then, we created a maze-like scene of shopping shelves to introduce a challenge to the player when finding items. We wanted some items to be unreachable from the ground level, so we introduced a few ways of getting on top of the shelves. This also gives players a safe space to hide/escape to.

![image](https://user-images.githubusercontent.com/36744690/197417182-e3c42b70-1b32-4724-8a89-8b6c5915ef36.png)

### Player Movement/Mechanics 

#### Walking & Jumping
We kept the walking and jumping simple but still physics based. All movement is done with WASD and the camera and looking around is controlled with the mouse.  

#### Climbing
We made it so there were some places onto which a player could jump or walk up to that allowed climbing up. This could be used to get a vantage point over the guards, or to get to a few tricky to reach items.

#### Meow
We added a meow mechanic which is manually controlled and useful for luring guards to a certain place. It also contributes to some tasks and achievements discussed later. If a guard hears a meow, then it will be immediately interested in the location where the meow occurred.

### Artificial Intelligence 

#### General
To begin with the AI, we baked our nav mesh so that the guards would be able to walk around the floor of the supermarket. The design was done with the AI created first as cylinders but defined as humanoids.

![image](https://user-images.githubusercontent.com/36744690/197417267-07ab9672-c204-4324-9188-96f8a7bab3e0.png)

#### Patrolling
For these guards, we had three different states.
In a patrol state, guards are set up to move to and from each respective patrol point, whilst also checking if the player is within their view every frame. Should the AI “spot” the player, it will move into a new chase state.

#### Chasing
When in a chase state, the guard will move to the last seen position of the player, at a higher speed than when patrolling. In fact, it is much faster than the player, and you need to be quick if you want to escape when spotted. If the guard reaches the last known position, does not catch and cannot see the player, it will move into a new search state.

#### Searching
Inside the search state, guards will check positions near the last seen point, but if more than 5 seconds has passed, they will return to patrolling. One thing we could improve upon here, would be fine-tuning where the guards will check within this state to avoid attempting to go to points that are too far away.

#### Distracted
When the player meows, we determine if the AI is within earshot before setting the AI’s target position to the player’s position at the time of the meow. Then, inside the search and patrol state, check when the AI has reached that position, and enter a new search state, looking for the source of the sound.

#### Animation
Once we were done with the code, we imported the player mesh from the lab test as well as some animations and set it up so that the guards would walk whilst patrolling, and run when chasing the player.

### General gameplay

#### Inventory
A complete item inventory system was built and then displayed on the HUD with item space limitations. This gives us the functionality to create tasks and achievements based on item retrieval and collection.

![image](https://user-images.githubusercontent.com/36744690/197417373-dcb1fb12-3704-4be2-b9d5-da2f4789aaf0.png)

#### Collectable Items
We took some of the items from the supermarket shelves and started laying some of them around the room, in spots where cats might be able to sneakily grab them without it being noticed they were missing. They include a particle effect to make them easier to spot as you are stealing.

![image](https://user-images.githubusercontent.com/36744690/197417420-4cd16f87-2534-424a-8ab5-c9a9161c514b.png)
 
#### Tasks
A completely customisable interface was designed to make different task types and allow easy extension of the gameplay. After playing around with the most logical ones, we settled with item retrieval, and a few fun ones (like meow a number of times… cats love to meow)!

![image](https://user-images.githubusercontent.com/36744690/197417434-0177003e-27fb-454e-baa2-a8e8c1756721.png)

### Title/Loading Screen

#### Design
We implemented a loading screen, which preloads audio and any global non-singleton systems, like the achievements system. This screen can be revisited after losing or completing a round, allowing a player to check achievements and the how to play screens again.

#### How to play
Although we tried to make the gameplay as intuitive as possible,we added a panel to explain to players what the game is all about, and how they should be playing it.

![image](https://user-images.githubusercontent.com/36744690/197417457-ef55df19-b82e-470a-894e-a76b7ca3355e.png)

#### Achievements
To make sure that the game remained fun to play a few times, and because we both are achievement hunters too, we decided to add a local achievements system. This made it fun to replay while attempting to complete everything on the list.

![image](https://user-images.githubusercontent.com/36744690/197417473-59a8df7c-16f3-4aa9-b414-f325c00a95e9.png)

### Sound/Lighting Design

#### Music
We have atmospheric rain playing upon start up in the loading screen, and our title music comes in once the game has loaded.
In the gameplay, we used a much more eery sounding track to add to the ambience.

#### SFX
We included sound effects for picking up items and winning the game. We also implemented a sound effect for when the player is spotted by the AI, to not only give the player a heads-up, but hopefully, get their hearts racing a little bit. There are some other sound effects such as returning items to the wheelbarrow and some indications noises.

#### Lighting
To go along with the uncertain, spooky feel, we have fog, and subtle global ambient lighting.

#### Particle effects
We added some simple particle systems to show the player where items were, and also in the more static parts of the game (like the title screen).

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
2. Some code is adapted from lecture and tutorial material from the Massey course 159.361.

##### _Sounds and Music_
1. Title screen music was created by Josh.

##### _Images, models and visual assets_
1. Many 3D models and related assets of the supermarket, were sourced from the paid asset pack [Polygon Town Pack] (which was paid for by Josh).
2. Some textures are taken from the [All Sky Free] pack.
3. Some free wall textures from [A dog's life software].
4. Some PBR boxes from [CrowArt].
5. A free lamp model from [ESsplashkid].
6. Some paid prototyping tools from [Polygon].

   [Unity]: <https://unity.com/>
   [Github LFS]: <https://git-lfs.github.com/>
   [Rider]: <https://www.jetbrains.com/rider/>
   [Visual Studio]: <https://visualstudio.microsoft.com/vs/>
   [Polygon Town Pack]: <https://syntystore.com/products/polygon-town-pack>
   [this link]: <https://answers.unity.com/questions/441246/editor-script-to-make-play-always-jump-to-a-start.html>
   [All Sky Free]: <https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014>
   [A dog's life software]: <https://assetstore.unity.com/packages/2d/textures-materials/brick/18-high-resolution-wall-textures-12567>
   [CrowArt]: <https://assetstore.unity.com/packages/3d/props/pbr-cardboard-box-110635>
   [ESsplashkid]: <https://assetstore.unity.com/packages/3d/props/electronics/old-ussr-lamp-110400>
   [Polygon]: <https://assetstore.unity.com/packages/3d/props/exterior/polygon-prototype-low-poly-3d-art-by-synty-137126>

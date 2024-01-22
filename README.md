# 8108_Game
# The-AprenticeKnight
 
This was our FPS stealth game inspired by SCP. The project had a deadline of less than a month, and the team needed to provide some form of intelligent AI, and an understanding of level design, and art using the Unity Engine. In the game, you play as Experiment 8108 also known as BloB by the dev team, and your job is to escape the faciity before being captured by the security drones. The project had a major constraint where we were only allowed two rooms that had to be a linear path to get from point A to point B.

## Table of Contents
- [Installation](#installation)
- [Features](#features)
- [Contributing](#contributing)
- [Acknowldgement](#acknowledgment)
- [Badges](#badges)
- [Debugging](#debugging)

## Installation
1. Clone repository ` bash git clone https://github.com/ACartaya96/The-AprenticeKnight.git ` 
2. Download at: 

### Features
●	We have implemented our 3 significant mechanics:
  
  ○	The Player Character. We wanted BloB to have more than the classic locomotion of a first-person profile game, so we decided to add more to his movement mechanics and give him the way to interact with the environment such as:
     
     ■	The ability to stick on surfaces one way BloB could get away from his pursuers was to climb walls, however, they could not do it forever as the player did have a stamina bar which would deplete the longer they would stay up on the walls.
     
     ■	BloB can get through vent grates to escape their pursuer this allowed BloB more versatility in the approach of the level, and helped our project stand out a little while keeping true to the constraints of the project.
     
     ■	BloB was also able to sneak up on its pursuers and was able to disable them from a switch on their back. This was another feature we decided to add to stand out, and we were even going to add that you need to disable these bots to gain access key to beat the level. However, due to time constraints we were never able to finish the final implementation.
  
  ○	Security Drones. The security drones use a Finite State Machine to control their behaviors as we only required three actions from them which were:
    
     ■	Patrol. The drones would patrol between points on the level that the designer could place on the map and drag onto the drone gameObject where they can create a list of points for it to follow. If it switches from this behavior at any point the drone will save its last location, and return to it once it returns to patrol mode.
     
     ■	Chase. The drone's simplest behavior was chase as when they see the player they begin to give chase to attempt to capture BloB.
      
     ■    Search. We wanted to drones to have an additional behavior as Patrol->Chase->Patrol felt too mechanical, and amateurish. We decided to make an intermedium mechanic where the drone will search a small area by saving the last known player location and searching around that area for a couple of seconds, before returning to patrol.
     
### Acknowledgment
Alexander Cartaya
Luis Estadas
Kevin Horan
Manny Pereira Valecillos

### Badges


### Debugging
●	BloB makes an extremely obnoxious noise when he moves it was fine until we found that there is a way to glitch the audio into a rapid-fire repetition which may need to issue a headphone warning.

● The wall crawl mechanics though done provide some difficulty, and has the tendency to clip through walls, however, the clipping is inconsistent and only on certain walls.

● The diable interaction button is inconsistent at times as the target of the interact button is no always the back switch on the bot, or even at time the switch can be flicked from the front of the robot at a certain angle of the reticle. We have no idea if the is interactable objects doing or if the reticle of our character is offset.

 

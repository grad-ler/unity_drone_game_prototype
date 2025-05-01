# Unity Drone Game Prototype
![Drone_Racer_Desktop_GIF_03](https://github.com/user-attachments/assets/5d0810ec-2a77-4afc-a004-14bf3ae001b9)
> **Drone Racer** is a split-screen local multiplayer drone racing prototype, developed in Unity as part of a Software Engineering project.
## 🕹️ Features
* Local split-screen multiplayer (2 players)
* Physics-based drone flight with support for pitch, roll, yaw, and lift
* Checkpoint-based racing system with visual indicators
* Countdown at race start and timer for both players
* Result screen with positions, finish times, and DNF status
* Support for keyboard and controller input
* Responsive UI with race timer, directional arrow, and restart options
* Implemented with Unity and the Unity Input System
## 📦 Project Goals
* The game is designed as a low-barrier introduction to drone control mechanics, focusing on:
* Spatial awareness and flight orientation
* Basic maneuvering with real drone-like dynamics
* Fun and competitive learning through racing gameplay
## 🧩 System Overview
### Architecture
- `LR_Game_Manager`: Manages game states, timers, and transitions.
- `LR_Drone_Controller`: Controls physics and drone movement logic.
- `LR_Drone_Inputs`: Captures player input using Unity's Input System.
- `LR_Drone_Engine`: Simulates force output from drone motors.
- `LR_Track_Checkpoints`: Controls race progress and triggers checkpoints.
### UI Manager
* Displays time, countdown, results
### Gameplay Mechanics
* Only the current checkpoint is visible
* A dynamic arrow guides players to the next one
* The second-place player has 5 seconds to finish after the first player completes the race
## 📌 What's Missing?
* ❌ Online multiplayer
* ❌ AI opponents
* ❌ Vehicle upgrades
* ❌ Saving best times
* ❌ Persistent data storage
## 🧪 Technical Overview
* **Engine**: Unity
* **Language**: C#
* **Architecture**: Component-based, event-driven
* **Input**: Unity Input System
* **Physics**: Rigidbody with custom force application
* **Rendering**: Dual camera setup for split-screen
## 📅 Development Timeline
* Concept Phase: Game design, control system draft
* Implementation: Feature development, continuous testing
* Finalization: Bug fixing, polishing, and documentation
## 🖼️ Media
### Main Menu
![Drone_Racer_Main_Menu_01](https://github.com/user-attachments/assets/f9af9d36-5d2b-4e79-bd64-2d8147a0893e)
### Game Start
![Drone_Racer_Main_Menu_14](https://github.com/user-attachments/assets/4ae302c8-8f0d-4ff9-add4-a4ddbf125d56)
### Gameplay
![Drone_Racer_Main_Menu_13](https://github.com/user-attachments/assets/60f3fc48-2831-424d-9e57-13a2e2143cfc)
### In-game Controls Overview
![3](https://github.com/user-attachments/assets/9570e911-eac9-414b-b48c-54eff9240258)
### Race Over Interace
![1232](https://github.com/user-attachments/assets/63c51b0c-f517-4871-8e73-0f3f7563dc1a)
## 🙌 References
- Unity Drone Controller Tutorial by Indie-Pixel - https://youtu.be/vUyAev7YAV8?list=PL5V9qxkY_RnLyWVtxIbWY0ihiyOaAYoRr
- Split Screen Game Tutorial by One Wheel Studio - https://onewheelstudio.com/blog/2022/2/3/split-screen-multi-player-with-unitys-new-input-system
- Unlit racing drone by pebegou - https://www.fab.com/listings/94279cd0-b30f-4bdd-b56f-a7e09fb78573
- Various 3d assets by Synthy Studios - https://syntystore.com
---
Project by Leander Renholzberger (gradler)
Developed as part of a university software engineering course.



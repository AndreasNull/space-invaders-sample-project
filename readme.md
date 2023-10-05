# Space Invaders - Sample Project

### by Andreas Diktyopoulos (andreas.diktyopoulos@gmail.com)

This is a comprehensive and fully operational Unity sample project designed to showcase various software development techniques and project organization practices specifically tailored for mobile platforms (Android).

This repository opts for a simplified approach by not utilizing Large File Storage (LFS) for asset files.

Check out my visual documentation in miro: https://miro.com/app/board/o9J_l14E-44=/?share_link_id=467085331155

## Tech Stack
- Unity 2020.3.24f1
- Zenject 9.2.0
- UniTask 2.2.5

## How To Play In Editor

1. Go to menu **Space Invaders -> Play**

- Use mouse to interact with UI elements.
- Use arrows/AD keys to move left right.
- Use space to fire!

## How To Play On Android Device

1. Build project
2. Install apk on an android device
3. Start app

Touch on UI elements to interact with them.

## Settings

### Game Settings

Go to menu **Space Invaders -> Game Settings**

| Property                       | Description                                                  |
| ------------------------------ | ------------------------------------------------------------ |
| **Game Installer**             |                                                              |
| Projectile Asset Path          | The projectile prefab addressable path                       |
| Enemy Asset Path               | The enemy prefab addressable path                            |
|                                |                                                              |
| **Game Stats Settings**        |                                                              |
| Save Path                      | The name of the file to save the players/game stats. This is going to be stored under `Application.persistentDataPath` folder. |
| Total High Scores Count        | The number of high scores to save.                           |
|                                |                                                              |
| **Player Settings**            |                                                              |
| **Health Settings**            |                                                              |
| Max Lives                      | The number of lives player start a game session.             |
| Invulnerable Duration          | The duration in seconds of player became invulnerable after a hit. |
| **Player Move Handler**        |                                                              |
| Move Speed                     | Player move speed.                                           |
| **Shooter Handler**            |                                                              |
| Speed                          | Player's projectile speed.                                   |
| Offset Distance                | Player's projectile init position offset based on current player position. |
| Max Shooter Interval           | How quick can project the next projectile.                   |
|                                |                                                              |
| **Enemy Settings**             |                                                              |
| **Spawn Handler**              |                                                              |
| Rows                           | Number of enemy's wave rows.                                 |
| Enemy Pre Row                  | Number of enemy's wave per row.                              |
| Row Distance                   | The vertical distance of each row.                           |
| Enemy Distance                 | The horizontal distance among enemies.                       |
| Enemy Type Per Row             | A list of enemy's configuration which defines the type of enemy (mesh & points) which will be spawn per row. |
| **Move Handler**               |                                                              |
| Move Interval                  | How often the enemy wave is going to do a next move.         |
| Move Interval Decrease Percent | The percentage of decreasing the enemy's wave move interval after the wave moves down a row. |
| Move Horizontal Distance       | The distance of the horizontal movement which the enemy wave will move at each move interval. |
| Move Vertical Distance         | The distance of the vertical movement which the enemy wave will move when will reach left/right limits. |
| **Shooter Handler**            |                                                              |
| Speed                          | Enemy's projectile speed.                                    |
| Offset Distance                | Enemy's projectile init position offset based on current player position. |
| Min shoot Interval             | Min duration before enemy's wave shoot another projectile.   |
| Max shoot Interval             | Max duration before enemy's wave shoot another projectile.   |

### Game Loader Config

Go to menu **Space Invaders -> Game Loader Config**

| Property                      | Description                                                  |
| ----------------------------- | ------------------------------------------------------------ |
| Wait After Loading Everything | Amount of time to wait after loading everything.             |
| Asset Label To Preload        | The addressable label which the `AssetManager` will use to preload all assets from the Asset Bundles. |

# Technology

In this test I demonstrate the use of variant software engineering patterns, Most importantly:

- Dependency Injection
  - Used in most of the codebase.
- Finite State Machines
  - Used to implement the **Game State Machine** and also used in  **Enemy Wave Move Handler** in order to demonstrate it's generic quality. 
- Messaging System - Listener/Observer pattern
  - Used to communication game messages. Loosely coupled as possible! Great to update stuff on UI.

## Dependency Injection

For dependency injection I'm using **Zenject** framework (https://github.com/modesttree/Zenject). I used it in most of the codebase except the UI.

### Zenject Modifications

- Provide custom method `AssetManagerProvider(path:string)` to support factory instantiation from my **AssetManager**.
- Provide custom `IPrefabProvider` class `AssetManagerProvider`  to support factory instantiation from my **AssetManager**.

## Simple State Machine

I implemented this from scratch for this project based on a previous advanced state machine implementation of mine.

This implementation is based on **Zenject** dependency injection architecture.

In this implementation we can not directly change the state of the state machine externally, we can only trigger it's parameters. Is very similar to Unity's Animator State Machine logic. Personally I prefer this implementation as the state machine is a black box and we play with it's parameters only. The state machine itself is responsible for the correct transitions.

- States
- Transitions
- Conditions
  - Trigger Condition
  - Func Condition
- Trigger Parameters

## Game State Manager (Game State Commands)

The **GameStateManager** is a **SimpleStateMachine** which controls the game state:

- Init Game State
- Main Menu State
- In Game State
- Game Over State

The **GameStateManager**  is listening to the **StateCommand** game messages:

- InitializationCompleted
- GoToMainMenu
- StartGameSession
- GameOver

and accordingly changes it's states.

## Messaging System

The messaging system is base on **prime31's MessageKit** (https://github.com/prime31/MessageKit), with additional extensions. It's very simple and clear!

# Gameplay Requirements Fulfillment

- Player should have limited lives 
    *:: Can be set in game settings*
- Enemies should come in waves indefinitely
- Player and enemies can shoot to each other
  - When player hits enemy it dies.
  - When enemy hits player it loses one live
  - *When player's projectile hits enemy's projectile both are eliminated* - I added this as it is part of the original game.
- The game ends when player loses all lives
- After player has been hit, he should become invulnerable for a few seconds
    *:: Can be set in game settings*
- There should be a top down 3D camera 
    *:: I added a orthographic camera*

# Technical Requirements Fulfillment

- Player progress should be stored locally on device 
    *:: the game saves a json file under the Application.persistanceDataPath. The name of the file can be defines in game settings.*
- Use Unity.Canvas for UI/HUD elements
- All gameplay related constants should be stored in form of a config file (Scriptable Object preferable) 
    *:: see game settings*
- Use Unity.Addressables for asset management *:: see AssetManager.cs*
  - (Optional) Organize your assets so that most of them could be downloaded separately in from of AssetBundle, leave only what's entirely necessary in the build 
  *:: All the assets are grouped into 2 asset bundles (Space Invaders Data, Space Invaders Scenes). The game build consists only of the init scene which via the GameLoader is loading all the assets from the asset bundles. Right now the asset bundles will be in streaming folder inside the build as we haven't configured a remote workflow.*
- Make use of a finite state machine (FSM) and dependency injection (DI). You can choose either any framework you like or write everything from scratch. 
    *:: See Zenject & custom State Machine.*
- (Optional) Make use of async/await syntax and UniTask when dealing with asynchronous stuff. 
    *:: see GameLoader.cs & InGameState.cs*

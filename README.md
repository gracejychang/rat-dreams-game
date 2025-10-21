# RatDreamsGame

## 🐭 About  
This game is inspired by classic "arcade" vertical shooter games, featuring a fun rat theme thanks to a recent trip to New York & one of my favourite animated films, Ratatouille.

This project was built in Unity version 6.

## 💻 How to Run The Game
### Running in Unity Editor  
1. Clone the repository.  
2. Open the project folder in Unity (version 6 recommended).  
3. Open the main scene (`Assets/Scenes/SampleScene`).  
4. Press the **Play** button in the editor to start the game.
5. Enjoy the game!

### Running a Built Executable  
1. Clone the repository
2. Open the project folder, `rat-dreams-game.
3. Depending on your platform:
    - **Windows**
        - open `Builds/Windows`  
        - open `RatDreamsGame_WindowsBuild` folder and open `RatDreamsGame.exe`.  
    - **Apple**
        - open `Build/MacOS`
        - double click on `RatDreamsGame_MacOSBuild` app
4. Enjoy the game!

**Note:** I only have a Mac and did not test the Windows build. If you run into troubles with a Windows build, I recommend opening the project in Unity.

## 🎮 Gameplay
You are a rat dreaming of abundance! Collect food, avoid traps, and gain power advantages by eating what a rat normally wouldn't.

### Game states
- on game start:
    - press `N` to **start a new game** (this will reset inventory, points, and power-ups)
    - press `L` to **load** from the existing game (this will read from `inventory.json`)
- press `P` to pause the game
    - while paused:
        - press `R` to **resume**
        - press `S` to **save and quit**
- on game over
    - press `R` to replay the game

### Game controls
- `←` / `→` Arrow Keys – Move the Rat left and right
- `Spacebar` – Fire a projectile (after collecting the 🌶️**Hot Sauce** PowerUp)
    - Once a Hot Sauce is collected, press Spacebar to activate the power.
    - The power lasts for 4 seconds before you need to use another hot sauce to power-up again.

PowerUps like **Hot Sauce** can enable new abilities — collect them to gain an advantage for a period of time!

## 🧠 Approach
I wanted to explore how inventory systems in games by building a simple, end-to-end game. I thought about a lot of games with cool inventory systems: Zelda, MarioKart, even those early 2000s web games where you have to dress up a character for a party or fashion show. I eventually landed on a system similar to Super Mario, a classic inventory based game where the player can earn points or gain/use powers based on collectible objects.

### Inventory System
My main goal was to make the system scalable, maintanable, and easy to work with inside Unity. I did this by keeping concerns separate and taking advantage of Unity's built-in **ScriptableObject**.

#### InventoryManager
The **InventoryManager** kept track of all inventory logic, such as total points, power ups, and the items that has been collected or used by the player.

#### Data Structure
I took advantage of Unity's  **ScriptableObject** to define data types, which allowed for the following:

**Scalability**: We can support or add new item types (e.g., FoodItem, PowerUpItem) without changing existing code.

**Modular**: Items are standalone assets, not tied to `GameObjects`, so can be reused across scenes.

**Editor-Friendly**: Values can be tweaked directly in the Unity Inspector.

**Efficient**: Uses less memory than scene-bound objects and keeps runtime logic clean.

#### Trade-Offs
To manage all available items, an **InventoryDatabase** acts as a central reference point for item definitions. While this results in a clean, extensible architecture, it also introduced added complexity — especially around saving and loading inventory data.

Specifically, serializing/deserializing the `inventory.json` file required handling two separate data layers: runtime inventory state and asset-based item definitions. To keep things manageable, the data was split into two clearly defined folders:

#### 📁 Items Folder (Assets/Scripts/Inventory/Items/)
Contains all **ScriptableObject** item definitions and the `InventoryDatabase` asset that links them together. This acts as the master list of items available to the game.

#### 📁 Data Folder (Assets/Scripts/Inventory/Data/)
Stores JSON files or other serialized data related to the player’s inventory state and item definitions. This allows saving/loading and debugging outside the Unity runtime if needed.

Additionally, it’s worth noting that **ScriptableObject** is Unity-specific — so if we ever need to migrate to a different engine or framework, the inventory architecture would need to be redesigned.

## 🛠️ Additional Systems  
- **Game State:** Manages and tracks the current state of the game (e.g., playing, paused, game over).  
- **Collisions:** Handles object interactions, such as pickups, damage, and power-ups.  
- **Object Spawner:** Randomly spawns game objects into the scene to keep gameplay dynamic.
- **Sprites:** Custom sprites created using [Pixilart](https://www.pixilart.com/draw).

### Why these additions?
I chose these additional systems because they represent a knowledge in basic game mechanics that can be used for both tightly-scoped, small games or in large, complex systems. As a developer, I always look to produce the "simplest" solution to complex problems. I think it is easy to over-engineer, but it is very difficult to design systems that are simple & easy to understand. I aim for this mindset because simplicity in software typically means it will be easier to understand, scale, or extend over time.

I’m also passionate about building UIs that feels intuitive to the user. A game like Flappy Bird, which is incredibly simple but captured many people's attentions, is something I find very interesting. With some knowledge and an interest in human-computer interaction, I designed this game to leverage existing mental models of other games. For example:
    - A familiar vertical gameplay format where objects fall toward the player
    - Arrow buttons to move players and use of the spacebar to attack — standard mechanics in many games

This approach aligns with key HCI principles such as recognition over recall & leveraging existing mental models, making the game more accessible and reducing the cognitive load for players.

## 📝 Reflection
This project reflects who I am as a developer: someone who values thoughtful design, robust architecture, and forward-thinking solutions. Many of the systems were built with scalability in mind — for example, several architectural decisions focused on how easy it would be to add new collectible items in the future. I also care about good user experience: I value intuitiveness and interfaces that are easy/fun to use. I care a lot about end users, and every experience I build to bring some type of delight or ease.

This project also represents who I am outside being a developer. I love expressing myself through creative endeavours, so it was only natural that I made custom sprites for this project. From visual arts and writing, to playing music and baking, these are all creative pursuits I have made outside of my work as a software developer.

I've found that game and AR/VR development to be one of the most fun and fulfilling ways to combine creativity and technology.

## 🚧 To Improve
**Unit Tests** Due to time contraints, unit tests weren't included.
**Max/Min Screen Width & Height** A lot of this is hardcoded - this should ideally be dynamic based on screen resolution or camera settings.

## 🐛 Found a Bug?
**See any odd behaviours?**
Feel free to open a PR describing the bug, and I'll get to fixing it!

## 🙏 Thanks for Reading!
Happy gaming 🚀

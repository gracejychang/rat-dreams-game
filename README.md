# RatDreamsGame

## ABOUT  
This game is inspired by classic "arcade" vertical shooter games, featuring a fun rat theme thanks to a recent trip to New York & one of my favourite animated films, Ratatouille.


## APPROACH  
I wanted to explore how inventory systems in games by building a simple, end-to-end game. I thought about a lot of games with cool inventory systems: Zelda, MarioKart, even those early 2000s web games where you have to dress up a character for a party or fashion show. I eventually landed on a system similar to Super Mario, a classic inventory based game where the player can earn points or gain/use powers based on collectible objects.

## Inventory System
My main goal was to make the system easy to scale and maintain. I did this by keeping concerns separate and taking advantage of Unity's built-in **ScriptableObject**.

### InventoryManager
The **InventoryManager** kept track of all inventory logic, such as total points, power ups, and all items that has been collected by the player.

### Data Structure
This system was built with scalability in mind, in particular 

By using **ScriptableObject** to define our data types, we've made the system:

**Easy to Scale**: We can support or add new item types (e.g., FoodItem, PowerUpItem) without changing existing code.

**Modular**: Items are standalone assets, not tied to GameObjects, so can be reused across scenes.

**Editor-Friendly**: Values can be tweaked directly in the Unity Inspector.

**Efficient**: Uses less memory than scene-bound objects and keeps runtime logic clean.

#### Trade-Offs
To manage all available items, an **InventoryDatabase** acts as a central reference point for item definitions. While this results in a clean, extensible architecture, it also introduces some added complexity — especially around saving and loading inventory data.

Specifically, serializing/deserializing the `inventory.json` file required handling two separate data layers: runtime inventory state and asset-based item definitions. To keep things manageable, the data was split into two clearly defined folders:

##### Items Folder (Assets/Scripts/Inventory/Items/)
Contains all **ScriptableObject** item definitions and the InventoryDatabase asset that links them together. This acts as the master list of items available to the game.

##### Data Folder (Assets/Scripts/Inventory/Data/)
Stores JSON files or other serialized data related to the player’s inventory state, item definitions, or game progression (if applicable). This allows saving/loading and debugging outside the Unity runtime if needed.

Additionally, it’s worth noting that **ScriptableObject** is Unity-specific — so if we ever need to migrate to a different engine or framework, the inventory architecture would need to be redesigned.

---

## Additional Systems  

- **Game State:** Manages and tracks the current state of the game (e.g., playing, paused, game over).  
- **Collisions:** Handles object interactions, such as pickups, damage, and power-ups.  
- **Object Spawner:** Randomly spawns game objects into the scene to keep gameplay dynamic.
- **Sprites:** Custom sprites created using [Pixilart](https://www.pixilart.com/draw).

---

## TO IMPROVE
**Unit Tests** Due to time contraints, I did not include unit tests in the code.
**Max/Min Screen Width & Height** A lot of this is hardcoded - this should ideally be dynamic based on screen resolution or camera settings.

## HOW TO RUN  

### Running in Unity Editor  
1. Clone the repository.  
2. Open the project folder in Unity (version 6.2 recommended).  
3. Open the main scene (`Assets/Scenes/MainScene.unity` or your main scene file).  
4. Press the **Play** button in the editor to start the game.

### Running a Built Executable  
1. Navigate to the `Builds/Windows` or `Builds/MacOS` folder in the repository.  
2. On Windows, run `RatDreamsGame.exe`.  
3. On MacOS, open the executable in the `Builds/MacOS` folder.  
4. Enjoy the game!

## GAME MECHANICS
- ← / → Arrow Keys – Move the Rat left and right
- Spacebar – Fire a projectile (only after collecting the Hot Sauce PowerUp)

PowerUps like Hot Sauce enable new abilities — collect them to gain an advantage!

---

**See any odd behaviours?**
Feel free to open a PR describing the bug, and I'll get to fixing it!

Happy gaming 🚀

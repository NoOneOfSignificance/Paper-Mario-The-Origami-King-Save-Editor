# Paper Mario: The Origami King Save Editor
A Save Editor for the recently released Paper Mario: The Origami King written in C#.

# Features
## Editors
* Stat Editor (HP, Max HP, Coins, Spent Coins, Confetti, Confetti Bag Capacity, and current Bibliofolds)
* Inventory Editor (Weapon and Item edits. Can change a weapon's used durability and chance of breaking on the next turn)
* Partner Editor (Can choose to have Folded Bowser, Bobby, Bone Goomba, Sombrero Guy, Spike, Professor Toad, Green Toad, Luigi, Bowser Jr., Kamek, and Normal Bowser | Some partners may stay after removing them depending on your game progression)
* Play Time Editor (Game's internal playtime counter)
* Gameover Count Editor
* Toad Point Editor
## Settings
* Automatically create save file backups in by enabling a setting
* Have the program remind you to create a save file backup on startup

# User Guide
## Prerequisites
* You must have a modded Switch in order to backup your saves and replace them with edited ones.
* If you have a modded Switch, you will need to install a save manager such as [Checkpoint](https://github.com/FlagBrew/Checkpoint/releases/tag/v3.7.4).
* Download and install the .NET Framework 4.7.2 Runtime.

## Steps
### Getting your Save Data
1. Use your Save Manager of choice and backup your save data for Paper Mario: The Origami King.
2. Locate your data00.bin file in your SD Card and make sure it's on your PC.
 a. Backup your data00.bin in case anything goes wrong during the editing process.
 
### Editing your Save File with the Tool
1. Download the latest version of the Paper Mario: The Origami King Save Editor from the [Releases page](https://github.com/zSupremoz/Paper-Mario-TOK-Save-Editor/releases)
2. Open the application.
 a. If you wish to enable automatic backups, go to File -> Settings, then click on Automatically Create Backups.
 b. You can also change the location where you want to store these backups at. By default, the program creates the backup at the same place your data00.bin is located.
3. Open the data00.bin with the application.
4. Start editing to your hearts content.
5. When you're done editing, click on File -> Save to save your edits. Then export your save file to your save manager.

# Risks
* Because increasing your Max HP increases your attack power, instakilling enemies that are required to be battled for game progression (such as the Shy Guy at the Earth Vellumental Temple) can render your save file as unusuable if you wish to progress further.
* If the game increases your HP to 1000 or above, your game will crash.

# Known Issues
* Some Partners in the Partner Editor don't work. The ones that don't work are labeled as so.
* Investigating an issue where the save file may become unreadable (to get around this, save your game then create a new backup in Checkpoint/JKSV then edit your new backup).

# Credits
* [zSupremoz](https://twitter.com/zSupremoz) - Main developer
* [Blue](https://twitter.com/1mBlueDabadee) - Taught me how to recalculate the CRC32B hash

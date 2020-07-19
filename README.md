# Paper Mario TOK Save Editor
 Save editor for Paper Mario: The Origami King.

## User Guide

 ### Prerequisites

  * Your Switch must be softmodded or you must have access to your game save data in order to use this program.
  * .NET Framework 4.7.2 Runtime must be installed. You can download it [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).
  * Have a backup of your save file (optional).

### Loading your Save File

 1. Use your backup manager of choice to dump your save data.
 
 2. Transfer your data00.bin to your PC.
  a. Create a backup of your data00.bin incase something goes wrong.

 3. Download the latest version of [Paper Mario TOK Save Editor].

 4. Open the application, then click File -> Open.

 5. Navigate to the location of your data00.bin, then open it.
 
### Information about Editing

 #### Stat Edits
 * HP - Represents the first part of your HP Count. Max value is 999 (will revert to 990 when you enter a battle).
 * Max HP - Represents the second part of your HP Count (Damage is increased based on your Max HP count). Max value is 999 (will revert to 990 when you enter a battle).
 * Coins - Represents how much coins you currently have. Max value is 999,999.
 * Coins Spent - Represents how much coins you have spent on the save. Max value is 999,999,999.
 * Current Confetti - Represents your remaining Confetti. Max value is 999,999. (Input is in a text box because the value is stored as a double).
 * Confetti Bag Capacity - Represents the maximum Confetti you can carry. Max value is 999,999.
 
 #### Bibliofolds
 * Checkboxes represent whether you have unlocked them in the game. Can be edited by simply checking or unchecking.
 
 #### Inventory Editor
 * Your inventory slots are shown off on the left, and the item stats for the selected inventory slot is on the right.
 * The inventory editor currently supports editing Weapons and Items.
 * You can change the item type by clicking on the dropdown box and selecting the item.
 * Used Endurance - Represents how much the Weapon has been used.
 * Used Breakrate - Represents the chance of the item breaking the next time it gets used in combat.
 * If you're changing an item to a weapon, you'll need to apply the changes before you can edit the Used Endurance and Used Breakrate.
 * Make sure you apply your item changes before Saving.
 
 #### Extra Stuff
 * You can change your current partner by selecting a Partner's name in the dropdown box.
 * You can edit how long you've been playing the game for. (Max Hour count is 999, Minute count is 59, and Second count is 59)
 
 #### Post-Editing
 * When you finished editing, click on File -> Save (or use Ctrl+Shift+S). Then export the file to your SD Card.

### To-Do List
* Add the ability to edit Useful Items, Game Flags, Museum Stats, and other progression related things.
* Make separate windows for editing certain things like Inventory.

### Credits
* zSupremoz - because I made the thing lol
* Blue - Helping me out with CRC32B Hash Calculation

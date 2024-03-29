# Choose Starting Location
FOR GAME VERSION 2.00 / 2.02 (**PRE TALES FROM THE FAR TERRITORY**)    
Allows you to choose your starting location when starting a new Sandbox game. 
The idea is to add more variety and to allow better roleplaying/storytelling options. 
It is designed to work alongside my other mods [ChooseStartingGear](https://github.com/GruffCassquatch/ChooseStartingGear) and [ChooseStartingCondition](https://github.com/GruffCassquatch/ChooseStartingConditions) for a completely custom start.       

* You can choose your location even on Interloper!
* Includes ALL regions, not just the usual new game options
* All regions and locations are also available as Random options

## The mod will not work on Challenges or Story Mode, it is only for new Survival sandbox games.


## Requirements
[MelonLoader](https://github.com/HerpDerpinstine/MelonLoader/releases/latest/download/MelonLoader.Installer.exe)

[ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings/releases)

## Installation:
1. Download ```ChooseStartingLocation.dll``` from [releases](https://github.com/GruffCassquatch/ChooseStartingLocation/releases)
2. Drop ```ChooseStartingLocation.dll``` into your Mods folder
3. If you are updating from an older version, delete the ```ChooseStartingLocation.json``` from your Mods folder as old json's can cause errors if the mod's Settings have been changed

## Uninstallation:
Delete ```ChooseStartingLocation.dll``` and ```ChooseStartingLocation.json``` files from your Mods folder

## Using The Mod
1. Open the ```Options``` menu
2. Open the ```Mod Settings``` menu
3. Scroll across to the ```Choose Starting Location``` menu
4. Mod Options:
	* ```Disabled:``` Mod is disabled; a new game will be completely unmodified
	* ```Custom Coordinates:``` You can enter in your own custom starting coordinates
		* Choose your starting Region
		* Enter you Coordinates and optionally, your Rotation
		* It is easier to move the slider to the rough number you want with the mouse, then use the left/right arrow keys on your keyboard to get to the exact number. You can also manually enter the numbers in the json file and then launch the game.
		* To easily find coordinates for a location, go to the spot you like and use the debug screenshot key ```F8``` (by default)
		* The screenshot should be saved to your Desktop (by default), open it
		* The 3rd line with 3 numbers in brackets are the coordinates; enter them in the same order as they appear (X, Y, Z)
		* The 4th line with 2 numbers in brackets is the rotation (the direction the player is facing); enter these as they appear (X, Y)
		* You can reach interesting locations using the ```fly``` console command with [DeveloperConsole](https://github.com/FINDarkside/TLD-Developer-Console), press ```space bar``` to land
	* ```Location List:``` You can choose from a list of Regions and Locations, or select Random
		* Select a starting Region
		* Select a starting Location from the list
		* Selecting Random for Region will select a random location from all locations included in the mod
		* Selecting a specific Region and then selecting Random for Location will select a random location from the list of all locations in that Region
5. Click ```CONFIRM``` to apply your changes or ```BACK``` to exit without applying changes
6. Start a new Survival Mode game as usual
	* It does not matter what you select when you get to the Region selection screen. If the mod is enabled, it will override any choice you make here
	* All other steps remain unchanged



## Feedback, Questions & Troubleshooting
* I am active on [The Long Dark Modding](https://discord.gg/QvFE7VV4WZ) Discord server
	* **General questions and feedback:** post in my channel #cass
		* Please note that many indoor locations CANNOT be added as they do not have unique scene names. It is too much work to add and maintain these locations!
		* I am open to adding more locations (within reason), contact me on Discord if you have a specific location you really want 
	* **Troubleshooting:** 
		* Post in my channel #cass or in #troubleshooting 
		* Or create an issue here on GitHub if you're not on Discord
		* While the mod is still in the testing phase, it will leave messages in your MelonLoader window. These are to help troubleshoot if something does not work as expected. You should not be worried about any messages unless they are RED.

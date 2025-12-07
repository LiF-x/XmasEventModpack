# LiFx Xmas Events

This mod has been made For Christmas!
It Automatically spawns 150 christmas Gifts in the world and deletes all gifts after the 10 min event.
The events will start every hour.

### Prerequisite
If you already have the Loot mod installed for Knools.
```diff
- DO NOT DIRECTLY UPLOAD IN THIS CASE!
```
- Just edit the lines in your original one you have already installed to include the new objects/xmas gifts, the Object ID is 4093

### Installation Instructions

To Install this mod, ensure you have the LiFx Framework installed [Download here](https://lifxmod.com/)

This is a modpack containing
- Events (new storage containers) In LiFx Folder - This adds multiple new
- Loot (same mod used for knool loot system)
- Xmas Mod (this is the auto object spawner and removal system)


## Before you begin ensure your server is turned off

1. To Use this modpack locate your game servers route directory folder, Which is the same location your art.zip file and the games .exe is

2. Upload the following 3 Folders to this folder and overwrite (save backups)
- data (Warning make sure uploading these do not effect your current mods, copy over what you need from these files before uploading).
- mods (Check the Loot mod which is located here and update the loottable to allow loot to be dropped in new gifts).
- yolauncher
  
3. Check your Servers dbexport folder Located in the LiFx Folder (Located in root directory)
 - Copy these files over to your yolauncher folder, you just downloaded in this pack and overwrite.
   
4. Run the createModpack.bat (must have 7zp installed)

5. Upload the new .zip file created to yo launcher

6. Turn on the server and Activate the new modpack on yo launcher website


## Ingame Commands

Start event with no timer
```
RampartGames_XmasEvent::startEvent();
```
Start event with timer
```
RampartGames_XmasEvent::triggerEventManually();
```
End event deleting all boxes
```
RampartGames_XmasEvent::endEvent();
```
Cancel/Start the currently scheduled automatic cycle

Prevent new cycles from being scheduled

:arrow_right: This stops the event from automatically repeating, but it does not stop an event that is already in progress.

true is for allowing schedule false is to stop it
```
XmasEventTick::setProcessTicks(true);
```
```
XmasEventTick::setProcessTicks(false);
```


 


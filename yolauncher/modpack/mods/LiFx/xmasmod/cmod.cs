/**
* <author>Warped ibun</author>
* <email>lifxmod@gmail.com</email>
* <url>lifxmod.com</url>
* <credits>Christophe Roblin <christophe@roblin.no> modification to make it yolauncher server modpack and lifxcompatible</credits>
* <description>introduced to Lif:YO a christmas mod</description>
* <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
*/

if (!isObject(RampartGames_XmasEvent))
{
    new ScriptObject(RampartGames_XmasEvent) {};
}

package RampartGames_XmasEvent
{
  function RampartGames_XmasEvent::setup() {
    LiFx::registerCallback($LiFx::hooks::onMaterialsLoad, RegisterMaterials, RampartGames_XmasEvent);

    // NEW: hook into chat messages
    LiFx::registerCallback($LiFx::hooks::onChatMessage, onChatClientMessage, RampartGames_XmasEvent);
  }

  function RampartGames_XmasEvent::RegisterMaterials() {
    LiFx::loadRecursivelyInFolder("yolauncher/modpack/mods/LiFx/xmasmod", "Sounds.cs");
  }

  // NEW: called when any chat message appears
  function RampartGames_XmasEvent::onChatClientMessage(%message)
  {
      // matches the text contained in the log line
      if (strstr(%message, "Xmas Event has started!") != -1)
      {
          // Play your custom sound
          sfxPlay("ChristmasSound");
      }
  }

  function RampartGames_XmasEvent::path() {
    return $Con::File;
  }
};
activatePackage(RampartGames_XmasEvent);
LiFx::registerCallback($LiFx::hooks::mods, setup, RampartGames_XmasEvent);

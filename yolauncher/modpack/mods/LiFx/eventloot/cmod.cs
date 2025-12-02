/**
* <author>Warped ibun</author>
* <email>lifxmod@gmail.com</email>
* <url>lifxmod.com</url>
* <credits>Christophe Roblin <christophe@roblin.no> modification to make it yolauncher server modpack and lifxcompatible</credits>
* <description>introduced to Lif:YO a christmas mod</description>
* <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
*/

if (!isObject(eventloot))
{
    new ScriptObject(eventloot)
    {
    };
}
package eventloot
{
  function eventloot::setup() {
    LiFx::registerCallback($LiFx::hooks::onMaterialsLoad, RegisterMaterials, eventloot);
  }

  function eventloot::RegisterMaterials() {
    LiFx::loadRecursivelyInFolder("yolauncher/modpack/mods/LiFx/eventloot", "materials.cs");
  }

  function eventloot::path() {
    return $Con::File;
  }
};
activatePackage(eventloot);
LiFx::registerCallback($LiFx::hooks::mods, setup, eventloot);
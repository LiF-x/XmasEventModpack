singleton Material(XmasGift_Ribbon)
{
   mapTo = "unmapped_mat";
   diffuseColor[0] = "0.693872 0.135678 0.298505 1";
   translucentBlendOp = "None";
   materialTag0 = "Weapon";
   materialTag1 = "FX";
};

singleton Material(XmasGift_RibbonBall)
{
   mapTo = "RibbonBall";
   diffuseColor[0] = "1 1 1 0";
   diffuseMap[0] = "yolauncher/modpack/mods/LiFx/eventloot/art/Textures/XmasGift/base.dds";
   translucentBlendOp = "None";
   materialTag0 = "Weapon";
};

singleton Material(Box)
{
   mapTo = "Box";
   diffuseMap[0] = "yolauncher/modpack/mods/LiFx/eventloot/art/Textures/XmasGift/gift.dds";
   materialTag0 = "Weapon";
};

singleton Material(newMaterial)
{
   mapTo = "RibbonDecoration";
   diffuseMap[0] = "yolauncher/modpack/mods/LiFx/eventloot/art/Textures/XmasGift/ribon.dds";
   materialTag1 = "FX";
   materialTag0 = "Weapon";
};

singleton Material(XmasGift_RibbonDecoration)
{
   mapTo = "Ribbon";
   diffuseColor[0] = "0.8 0.200824 0.677447 1";
   translucentBlendOp = "None";
   materialTag0 = "RoadAndPath";
   diffuseMap[0] = "yolauncher/modpack/mods/LiFx/eventloot/art/Textures/XmasGift/ribondec.dds";
   materialTag1 = "FX";
};

singleton Material(Teleporter_Glow_Main)
{
   mapTo = "EggSkin";
   diffuseMap[0] = "yolauncher/modpack/mods/LiFx/eventloot/art/Textures/EasterEgg/Easter_egg_Diffuse.dds";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "1";
   glow[0] = "1";
   emissive[0] = "1";
   doubleSided = "1";
   animFlags[0] = "0x00000001";
   waveFreq[0] = "0.781";
   waveAmp[0] = "0.203";
   castShadows = "0";
   translucent = "0";
   translucentBlendOp = "Add";
   subSurface[0] = "1";
   showFootprints = "0";
   showDust = "1";
   materialTag0 = "Weapon";
};
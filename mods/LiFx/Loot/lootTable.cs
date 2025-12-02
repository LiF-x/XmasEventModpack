
///////////////////////////////////////Loot Table /////////////////////////////////////////////
// Add dbi Update commands to populate the loot table for the mod as shown in the below example
//
//  Chance is relative to each other, 0 means it is not lootable (disabled)
// 
//  Example:
//  Drop 1 has Chance 1
//  Drop 2 has Chance 2
//
//  Drop 2 is then twice more likely to be dropped than Drop 1 (2/1)
// 
// dbi.Update("INSERT IGNORE `" @ LiFxLoot::loottable() @ "` VALUES (ContainerID, ItemDropID, Min Quality, Max Quality, Min Quantity, Max Quantity, Chance)");
// dbi.Update("INSERT IGNORE `" @ LiFxLoot::loottable() @ "` VALUES (4093, 40, 10, 80, 1, 1, 1)");
dbi.Update("INSERT IGNORE `" @ LiFxLoot::loottable() @ "` VALUES (4093, 1415, 10, 80, 1, 1, 1)");
dbi.Update("INSERT IGNORE `" @ LiFxLoot::loottable() @ "` VALUES (4093, 1026, 10, 80, 1, 10, 2)");
dbi.Update("INSERT IGNORE `" @ LiFxLoot::loottable() @ "` VALUES (4093, 1059, 10, 80, 1, 10, 1)");
dbi.Update("INSERT IGNORE `" @ LiFxLoot::loottable() @ "` VALUES (4093, 1427, 10, 80, 1, 10, 1)");
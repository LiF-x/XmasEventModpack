/**
 * <author>Christophe Roblin</author>
 * <email>lifxmod@gmail.com</email>
 * <url>lifxmod.com</url>
 * <credits>Christophe Roblin <christophe@roblin.no></credits>
 * <description>Loot functionality for Life is Feudal: Your Own, works on new containers</description>
 * <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
 */

if (!isObject(Events))
{
    new ScriptObject(Events) { };
}

package Events
{
    function Events::setup() {
        LiFx::registerCallback($LiFx::hooks::onInitServerDBChangesCallbacks, ConChanges, Events); //Conversion setup
        LiFx::registerObjectsTypes(Events::objectstypescat(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesnewguildkit(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesStarterkit(), Events);
        LiFx::registerObjectsTypes(Events::objectstypeseventkit(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesgifts(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesXmasgifts(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesXmasgiftscarry(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesEasterEgg(), Events);
        LiFx::registerObjectsTypes(Events::objectstypesEasterEggcarry(), Events);

        LiFx::registerRecipe(Events::RecipeNewGuildKit(), Events);
        LiFx::registerRecipe(Events::RecipeStarterKit(), Events);
        LiFx::registerRecipe(Events::RecipeEventKit(), Events);
        LiFx::registerRecipe(Events::RecipeGifts(), Events);

    }

    function Events::loottable() {
    return "LiFxStarterKits";
}

  function Events::version() {
    return "1.0.0";
} 

  function Events::objectstypescat() {
        return new ScriptObject(objectstypescat: ObjectsTypes)
        {
        id = 4094; // has to be globally unique
        ObjectName = "LiFx Boxes";
        ParentID = 12; //old 60 - 69 storage maybe 63
        IsContainer = 0;
        IsMovableObject = 0;
        IsUnmovableObject = 0;
        IsTool = 0;
        IsDevice = 0;
        IsDoor = 0;
        IsPremium = 0;
        MaxContSize = 0;
        Length = 0;
        MaxStackSize = 0;
        UnitWeight = 0;
        BackgrndImage = 0;
        WorkAreaTop = 0;
        WorkAreaLeft = 0;
        WorkAreaWidth = 0;
        WorkAreaHeight = 0;
        BtnCloseTop = 0;
        BtnCloseLeft = 0;
        FaceImage = "";
        Description = "LiFx Mod Boxes";
        BasePrice = 36500;
        OwnerTimeout = NULL;
        AllowExportFromRed = 0;
        AllowExportFromGreen = 0;
    };
}

  function Events::objectstypesXmasgifts() {
      return new ScriptObject(objectstypesXmasgifts: ObjectsTypes)
      {
      id = 4093; // has to be globally unique
      ObjectName = "Christmas Gift";
      ParentID = 4094;
      IsContainer = 1;
      IsMovableObject = 1;
      IsUnmovableObject = 0;
      IsTool = 0;
      IsDevice = 0;
      IsDoor = 0;
      IsPremium = 0;
      MaxContSize = 1500000;
      Length = 10;
      MaxStackSize = 0;
      UnitWeight = 1000;
      BackgrndImage = "art/images/universal";
      WorkAreaTop = 0;
      WorkAreaLeft = 0;
      WorkAreaWidth = 0;
      WorkAreaHeight = 0;
      BtnCloseTop = 0;
      BtnCloseLeft = 0;
      FaceImage = "";
      Description = "LiFx Mod Boxes";
      BasePrice = 2300;
      OwnerTimeout = 150;
      AllowExportFromRed = 0;
      AllowExportFromGreen = 0;
      };
    }

   function Events::objectstypesXmasgiftscarry() {
      return new ScriptObject(objectstypesXmasgiftscarry: ObjectsTypes)
      {
      id = 4092; // has to be globally unique
      ObjectName = "Christmas Gift"; // carry object
      ParentID = 1902;
      IsContainer = 0;
      IsMovableObject = 0;
      IsUnmovableObject = 0;
      IsTool = 0;
      IsDevice = 0;
      IsDoor = 0;
      IsPremium = 0;
      MaxContSize = 10000;
      Length = 10;
      MaxStackSize = 1;
      UnitWeight = 5000;
      BackgrndImage = "art/images/universal";
      WorkAreaTop = 0;
      WorkAreaLeft = 0;
      WorkAreaWidth = 0;
      WorkAreaHeight = 0;
      BtnCloseTop = 0;
      BtnCloseLeft = 0;
      FaceImage = "";
      Description = "LiFx Mod Boxes";
      BasePrice = NULL;
      OwnerTimeout = NULL;
      AllowExportFromRed = 0;
      AllowExportFromGreen = 0;
      };
    }

  function Events::objectstypesEasterEgg() {
      return new ScriptObject(objectstypesEasterEgg: ObjectsTypes)
      {
      id = 4091; // has to be globally unique
      ObjectName = "Easter Egg";
      ParentID = 4094;
      IsContainer = 1;
      IsMovableObject = 1;
      IsUnmovableObject = 0;
      IsTool = 0;
      IsDevice = 0;
      IsDoor = 0;
      IsPremium = 0;
      MaxContSize = 1500000;
      Length = 10;
      MaxStackSize = 0;
      UnitWeight = 1000;
      BackgrndImage = "art/images/universal";
      WorkAreaTop = 0;
      WorkAreaLeft = 0;
      WorkAreaWidth = 0;
      WorkAreaHeight = 0;
      BtnCloseTop = 0;
      BtnCloseLeft = 0;
      FaceImage = "";
      Description = "LiFx Mod Boxes";
      BasePrice = 2300;
      OwnerTimeout = 150;
      AllowExportFromRed = 0;
      AllowExportFromGreen = 0;
      };
    }

   function Events::objectstypesEasterEggcarry() {
      return new ScriptObject(objectstypesEasterEggcarry: ObjectsTypes)
      {
      id = 4090; // has to be globally unique
      ObjectName = "Easter Egg"; // carry object
      ParentID = 1902;
      IsContainer = 0;
      IsMovableObject = 0;
      IsUnmovableObject = 0;
      IsTool = 0;
      IsDevice = 0;
      IsDoor = 0;
      IsPremium = 0;
      MaxContSize = 10000;
      Length = 10;
      MaxStackSize = 1;
      UnitWeight = 5000;
      BackgrndImage = "art/images/universal";
      WorkAreaTop = 0;
      WorkAreaLeft = 0;
      WorkAreaWidth = 0;
      WorkAreaHeight = 0;
      BtnCloseTop = 0;
      BtnCloseLeft = 0;
      FaceImage = "";
      Description = "LiFx Mod Boxes";
      BasePrice = NULL;
      OwnerTimeout = NULL;
      AllowExportFromRed = 0;
      AllowExportFromGreen = 0;
      };
    }        

  function Events::objectstypesnewguildkit() {
      return new ScriptObject(objectstypesnewguildkit: ObjectsTypes)
      {
      id = 2578; // has to be globally unique
      ObjectName = "New guild kit";
      ParentID = 4094;
      IsContainer = 1;
      IsMovableObject = 1;
      IsUnmovableObject = 0;
      IsTool = 0;
      IsDevice = 0;
      IsDoor = 0;
      IsPremium = 0;
      MaxContSize = 250000;
      Length = 7;
      MaxStackSize = 0;
      UnitWeight = 5000;
      BackgrndImage = "art/images/universal";
      WorkAreaTop = 0;
      WorkAreaLeft = 0;
      WorkAreaWidth = 0;
      WorkAreaHeight = 0;
      BtnCloseTop = 0;
      BtnCloseLeft = 0;
      FaceImage = "art/2d/objects/chest.png";
      Description = "LiFx Mod Boxes";
      BasePrice = 2300;
      OwnerTimeout = 150;
      AllowExportFromRed = 0;
      AllowExportFromGreen = 0;
  	};
  }

    function Events::objectstypesStarterkit() {
    return new ScriptObject(objectstypesStarterkit: ObjectsTypes)
    {
    id = 2579; // has to be globally unique
    ObjectName = "Starter kit";
    ParentID = 4094;
    IsContainer = 1;
    IsMovableObject = 1;
    IsUnmovableObject = 0;
    IsTool = 0;
    IsDevice = 0;
    IsDoor = 0;
    IsPremium = 0;
    MaxContSize = 250000;
    Length = 7;
    MaxStackSize = 0;
    UnitWeight = 5000;
    BackgrndImage = "art/images/universal";
    WorkAreaTop = 0;
    WorkAreaLeft = 0;
    WorkAreaWidth = 0;
    WorkAreaHeight = 0;
    BtnCloseTop = 0;
    BtnCloseLeft = 0;
    FaceImage = "art/2d/objects/chest.png";
    Description = "LiFx Mod Boxes";
    BasePrice = 2300;
    OwnerTimeout = 150;
    AllowExportFromRed = 0;
    AllowExportFromGreen = 0;
    };
  }

    function Events::objectstypeseventkit() {
    return new ScriptObject(objectstypeseventkit: ObjectsTypes)
    {
    id = 2580; // has to be globally unique
    ObjectName = "Event kit";
    ParentID = 4094;
    IsContainer = 1;
    IsMovableObject = 1;
    IsUnmovableObject = 0;
    IsTool = 0;
    IsDevice = 0;
    IsDoor = 0;
    IsPremium = 0;
    MaxContSize = 250000;
    Length = 7;
    MaxStackSize = 0;
    UnitWeight = 5000;
    BackgrndImage = "art/images/universal";
    WorkAreaTop = 0;
    WorkAreaLeft = 0;
    WorkAreaWidth = 0;
    WorkAreaHeight = 0;
    BtnCloseTop = 0;
    BtnCloseLeft = 0;
    FaceImage = "art/2d/objects/chest.png";
    Description = "LiFx Mod Boxes";
    BasePrice = 2300;
    OwnerTimeout = 150;
    AllowExportFromRed = 0;
    AllowExportFromGreen = 0;
};
    }

    function Events::objectstypesgifts() {
    return new ScriptObject(objectstypesgifts: ObjectsTypes)
    {
    id = 2584; // has to be globally unique
    ObjectName = "Gifts";
    ParentID = 4094;
    IsContainer = 1;
    IsMovableObject = 1;
    IsUnmovableObject = 0;
    IsTool = 0;
    IsDevice = 0;
    IsDoor = 0;
    IsPremium = 0;
    MaxContSize = 250000;
    Length = 7;
    MaxStackSize = 0;
    UnitWeight = 5000;
    BackgrndImage = "art/images/universal";
    WorkAreaTop = 0;
    WorkAreaLeft = 0;
    WorkAreaWidth = 0;
    WorkAreaHeight = 0;
    BtnCloseTop = 0;
    BtnCloseLeft = 0;
    FaceImage = "art/2d/objects/chest.png";
    Description = "LiFx Mod Boxes";
    BasePrice = 2300;
    OwnerTimeout = 150;
    AllowExportFromRed = 0;
    AllowExportFromGreen = 0;
};
    }

  function Events::RecipeNewGuildKit() {
    %recipe =  new ScriptObject(RecipeNewGuildKit : Recipes)
    {
      RecipeName = "New Guild kit";
      Description = "Guild kit contact a member of the gm team to receive Norse Realms dollar so you can build this";
      StartingToolsID = 32;
      SkillTypeID = 18;
      SkillLevel = 60;
      ResultObjectTypeID = 2578;
      SkillDepends = 20;
      Quantity = 1;
      Autorepeat = 0;
      IsBlueprint = 0;
      ImagePath = "art/2D/Recipes/chest.png";
      Requirements = JettisonArray("RecipeNewGuildKitRequirements");

    };
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 32;
        Quality = 0;
        Influence = 10;
        Quantity = 20;
        IsRegionalItemRequired = 0; 
    });
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 1062;
        Quality = 0;
        Influence = 70;
        Quantity = 4;
        IsRegionalItemRequired = 0; 
    });
    return %recipe;
  }

  function Events::RecipeStarterKit() {
    %recipe =  new ScriptObject(RecipeStarterKit : Recipes)
    {
      RecipeName = "Starter kit";
      Description = "Starter kit contact a member of the gm team to receive Norse Realms dollar so you can build this";
      StartingToolsID = 32;
      SkillTypeID = 18;
      SkillLevel = 60;
      ResultObjectTypeID = 2579;
      SkillDepends = 20;
      Quantity = 1;
      Autorepeat = 0;
      IsBlueprint = 0;
      ImagePath = "art/2D/Recipes/chest.png";
      Requirements = JettisonArray("RecipeStarterKitRequirements");

    };
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 32;
        Quality = 0;
        Influence = 5;
        Quantity = 15;
        IsRegionalItemRequired = 0; 
    });
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 1062;
        Quality = 0;
        Influence = 75;
        Quantity = 2;
        IsRegionalItemRequired = 0; 
    });
    return %recipe;
  }

  function Events::RecipeEventKit() {
    %recipe =  new ScriptObject(RecipeEventKit : Recipes)
    {
      RecipeName = "Event kit";
      Description = "Event kit contact a member of the gm team to receive Norse Realms dollar so you can build this";
      StartingToolsID = 32;
      SkillTypeID = 18;
      SkillLevel = 60;
      ResultObjectTypeID = 2580;
      SkillDepends = 20;
      Quantity = 1;
      Autorepeat = 0;
      IsBlueprint = 0;
      ImagePath = "art/2D/Recipes/chest.png";
      Requirements = JettisonArray("RecipeEventKitRequirements");

    };
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 32;
        Quality = 0;
        Influence = 5;
        Quantity = 15;
        IsRegionalItemRequired = 0; 
    });
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 1062;
        Quality = 0;
        Influence = 75;
        Quantity = 2;
        IsRegionalItemRequired = 0; 
    });
    return %recipe;
  }

  function Events::RecipeGifts() {
    %recipe =  new ScriptObject(RecipeGifts: Recipes)
    {
      RecipeName = "Believer kit";
      Description = "Believer kit contact a member of the gm team to receive Norse Realms dollar so you can build this";
      StartingToolsID = 32;
      SkillTypeID = 18;
      SkillLevel = 60;
      ResultObjectTypeID = 2584;
      SkillDepends = 20;
      Quantity = 1;
      Autorepeat = 0;
      IsBlueprint = 0;
      ImagePath = "art/2D/Recipes/chest.png";
      Requirements = JettisonArray("RecipeGiftsRequirements");

    };
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 32;
        Quality = 0;
        Influence = 5;
        Quantity = 15;
        IsRegionalItemRequired = 0; 
    });
    %recipe.Requirements.Push(RecipeRequirements, new ScriptObject("" : RecipeRequirements){ 
        MaterialObjectTypeID = 1062;
        Quality = 0;
        Influence = 75;
        Quantity = 2;
        IsRegionalItemRequired = 0; 
    });
    return %recipe;
  }

    function Events::ConChanges() {
      }
};

activatePackage(Events);

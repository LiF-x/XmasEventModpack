/**
 * <author>Christophe Roblin</author>
 * <email>lifxmod@gmail.com</email>
 * <url>lifxmod.com</url>
 * <credits>Christophe Roblin <christophe@roblin.no></credits>
 * <description>Loot functionality for Life is Feudal: Your Own, works on new containers</description>
 * <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
 */
if (!isObject(RampartGames_XmasEvent)) {
    new ScriptObject(RampartGames_XmasEvent) { };
}
if (!isObject(RampartGames_XmasEventEventTick)) {
    new ScriptObject(RampartGames_XmasEventEventTick) { };
}

if ($RampartGames::XmasEvent::DataPath $= "")
{
    $RampartGames::XmasEvent::DataPath = filePath($Con::File);
    echo("XmasEvent Data Path:" SPC $RampartGames::XmasEvent::DataPath);
}

package RampartGames_XmasEvent {

    function RampartGames_XmasEvent::setup()
    {
        // Load previous spawn list and schedule cleanup
        RampartGames_XmasEvent::cleanupSavedSpawns();

        LiFx::registerCallback($LiFx::hooks::onPostInitCallbacks, startSpawn, RampartGames_XmasEvent);
        LiFx::registerCallback($LiFx::hooks::onPostInitCallbacks, loadSpawnList, RampartGames_XmasEvent);
    }

    function RampartGames_XmasEvent::version()
    {
        return "0.0.1";
    }

    function RampartGames_XmasEvent::triggerObjectspawn(%this, %num)
    {
        echo(%this, %num);

        %minX = -3060; %maxX = 3060;
        %minY = -3060; %maxY = 3060;

        %message = "<spop><spush><color:c6935f>Santa<spop> has dropped off presents near, "
                   @ "<spop><spush><color:c6935f>GeoID: " @ %TargetGeoID
                   @ "<spop> Go and find the gifts before someone else does!";

        if((!%num || !$RampartGames::XmasEvent::Enabled || !$RampartGames::XmasEvent::intervalInMs)
           || (%num >= $RampartGames::XmasEvent::maxObjects))
        {
            echo("Number of objects to spawn not specified" SPC %num);
            RampartGames_XmasEventEventTick::setProcessTicks(false);
            RampartGames_XmasEventEventTick::setProcessTicks(true);
            return;
        }

        while(!%position)
        {
            %x = getRandom(%minX, %maxX);
            %y = getRandom(%minY, %maxY);
            %position = isValidPosition(%x, %y);
        }

        %geoID = LiFxUtility::getGeoID(%position);
        %container = createTestMovable(4093, %geoID, 0, 2);

        if(!%container)
        {
            echo("Failed to spawn object with Geo ID:" SPC %geoID);
        }
        else
        {
            echo("CONTAINER ID?" SPC %container);

            // Save the spawned object persistently
            RampartGames_XmasEvent::recordSpawn(%container, %geoID, %position);

            // Schedule container cleanup 5 minutes later
            %this.schedule(300000, "cleanupContainer", %container);

            // Schedule file cleanup 5 minutes later
            %this.schedule(300000, "deleteAllFromFile");

            LiFxUtility::messageAll(2480, %message);
        }

        %this.schedule(5000, "triggerObjectspawn", %num + 1);
    }

    function RampartGames_XmasEvent::recordSpawn(%this, %container, %geoID, %pos)
    {
        $RampartGames::XmasEvent::SpawnedList[$RampartGames::XmasEvent::SpawnedListCount] = %container;
        $RampartGames::XmasEvent::SpawnedListCount++;

        RampartGames_XmasEvent::saveSpawnList();
    }

    function RampartGames_XmasEvent::saveSpawnList()
    {
        %folder = $RampartGames::XmasEvent::DataPath @ "/XmasData";

        if (!isDirectory(%folder))
        {
            createPath(%folder @ "/dummy.txt");
            fileDelete(%folder @ "/dummy.txt");
        }

        %path = %folder @ "/XmasObjects.cs";

        %file = new FileObject();
        if (%file.openForWrite(%path))
        {
            echo("Saving Xmas GeoIDs to:" SPC %path);

            for (%i = 0; %i < $RampartGames::XmasEvent::SpawnedListCount; %i++)
            {
                %geoID = $RampartGames::XmasEvent::SpawnedList[%i];
                %file.writeLine(%geoID);
            }

            %file.close();
        }
        %file.delete();
    }

    function RampartGames_XmasEvent::loadSpawnList()
    {
        %path = $RampartGames::XmasEvent::DataPath @ "/XmasData/XmasObjects.cs";
        %file = new FileObject();

        if (!%file.openForRead(%path))
        {
            echo("No saved XmasObjects.cs found:" SPC %path);
            $RampartGames::XmasEvent::SpawnedListCount = 0;
            return;
        }

        echo("Loading saved Xmas objects from:" SPC %path);

        $RampartGames::XmasEvent::SpawnedListCount = 0;

        while (!%file.isEOF())
        {
            %line = %file.readLine();
            if (%line $= "")
                continue;

            $RampartGames::XmasEvent::SpawnedList[$RampartGames::XmasEvent::SpawnedListCount] = %line;
            $RampartGames::XmasEvent::SpawnedListCount++;
        }

        %file.close();
        %file.delete();

        echo("Loaded" SPC $RampartGames::XmasEvent::SpawnedListCount SPC "saved Xmas containers.");
    }

    function RampartGames_XmasEvent::cleanupSavedSpawns(%this)
    {
        echo("Cleaning up saved Xmas spawns...");

        for (%i = 0; %i < $RampartGames::XmasEvent::SpawnedListCount; %i++)
        {
            %geoID = $RampartGames::XmasEvent::SpawnedList[%i];
            if (%geoID !$= "")
            {
                echo("Removing old Xmas container:" SPC %geoID);
                DeleteTestMovable(%geoID);
            }
        }

        $RampartGames::XmasEvent::SpawnedListCount = 0;

        // Schedule deletion of the file 5 minutes later
        %this.schedule(300000, "deleteAllFromFile");

        echo("Scheduled deletion of XmasObjects.cs in 5 minutes.");
    }

    function RampartGames_XmasEvent::deleteAllFromFile()
    {
        %folder = $RampartGames::XmasEvent::DataPath @ "/XmasData";
        %path = %folder @ "/XmasObjects.cs";

        %file = new FileObject();
        if (!%file.openForRead(%path))
        {
            %file.delete();
            return;
        }

        echo("Deleting all Xmas objects listed in:" SPC %path);

        while (!%file.isEOF())
        {
            %geoID = %file.readLine();
            if (%geoID $= "")
                continue;

            DeleteTestMovable(%geoID);
            echo("Deleted object with GeoID:" SPC %geoID);
        }

        %file.close();
        %file.delete();

        // Clear the file
        %file = new FileObject();
        if (%file.openForWrite(%path))
            %file.close();
        %file.delete();

        $RampartGames::XmasEvent::SpawnedListCount = 0;
        echo("All Xmas objects removed and XmasObjects.cs cleared.");
    }

    function RampartGames_XmasEvent::cleanupContainer(%this, %container)
    {
        if (!isObject(%container))
            return;

        echo("Starting decay for container:" SPC %container);

        %cleanupmsg = "<spop><spush><color:c6935f>Server:<spop> has started the xmas cleanup, "
                      @ "Go and find the gifts before they are removed!";

        LiFxUtility::messageAll(2480, %cleanupmsg);

        %container.deleteAllFromFile();
    }

    function isValidPosition(%x, %y)
    {
        %z = LiFxUtility::getTerrainHeightVector(%x);

        %rayStart = %x SPC %y SPC 1500;
        %rayEnd   = %x SPC %y SPC (%z - 100);
        %mask     = $TypeMasks::WaterObjectType;

        %result = containerRayCast(%rayStart, %rayEnd, %mask);
        if (%result)
        {
            echo("Invalid water position:" SPC %x SPC %y SPC %z);
            return false;
        }

        return %x SPC %y SPC %z;
    }

    function RampartGames_XmasEvent::startSpawn(%this)
    {
        RampartGames_XmasEventEventTick::setProcessTicks(true);
    }

    function RampartGames_XmasEventEventTick::setProcessTicks(%bool)
    {
        echo("EventTick:" SPC %bool);

        if (%bool)
        {
            RampartGames_XmasEventEventTick.eventID =
                RampartGames_XmasEventEventTick.schedule($RampartGames::XmasEvent::intervalInMs, onProcessTick);
        }
        else
        {
            cancel(RampartGames_XmasEventEventTick.eventID);
        }
    }

    function RampartGames_XmasEventEventTick::onProcessTick(%this)
    {
        RampartGames_XmasEvent::triggerObjectspawn(RampartGames_XmasEvent, 1);
        %this.eventID = %this.schedule($RampartGames::XmasEvent::intervalInMs, onProcessTick);
    }
};

activatePackage(RampartGames_XmasEvent);

/**
 * <author>Christophe Roblin and warpedIbun</author>
 * <email>lifxmod@gmail.com</email>
 * <url>lifxmod.com</url>
 * <credits>Christophe Roblin <christophe@roblin.no></credits>
 * <description>Christmas Event functionality for Life is Feudal: Your Own, works on new containers and requires a loot system</description>
 * <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
 */

if (!isObject(RampartGames_XmasEvent)) {
    new ScriptObject(RampartGames_XmasEvent) { };
}
if (!isObject(XmasEventTick)) {
    new ScriptObject(XmasEventTick) { };
}

$RampartGames::XmasEvent::DataPath = ($RampartGames::XmasEvent::DataPath $= "" ? filePath($Con::File) : $RampartGames::XmasEvent::DataPath);
$RampartGames::XmasEvent::EventEnabled = true;
$RampartGames::XmasEvent::EventIntervalMs = 3600000; // 1 hour by default
$RampartGames::XmasEvent::EventDurationMs = 600000;  // 5 minutes
$RampartGames::XmasEvent::NumGifts = 150;

package RampartGames_XmasEvent {

    function RampartGames_XmasEvent::setup() {
        RampartGames_XmasEvent::cleanupSavedSpawns();
        LiFx::registerCallback($LiFx::hooks::onStartCallbacks, OnstartActivation, RampartGames_XmasEvent);
    }

    function XmasEventTick::onProcessTick(%this) {
        %interval = $RampartGames::XmasEvent::EventIntervalMs;

        // Schedule pre-event warnings
        %this.schedule(%interval - 180000, "sendWarning", 3);   // 3 min
        %this.schedule(%interval - 60000,  "sendWarning", 1);   // 1 min
        %this.schedule(%interval - 30000,  "sendWarning", 0.5); // 30 sec

        // Start event **after countdown finishes**
        %this.schedule(%interval, "startEventManual");

        // Schedule next tick **only after this event ends**
        %this.eventID = %this.schedule(%interval + $RampartGames::XmasEvent::EventDurationMs + 1000, onProcessTick);
    }

    function RampartGames_XmasEvent::OnstartActivation() {
        echo("Onstart Xmas Event Triggered!");
        // Cancel any automated scheduling to prevent double triggers
        cancel(XmasEventTick.eventID);

        %this = "XmasEventTick";

        // Send warnings
        %this.sendWarning(3);                 // 3 min
        %this.schedule(120000, "sendWarning", 1);   // 1 min
        %this.schedule(150000, "sendWarning", 0.5); // 30 sec

        // Start event after 3 minutes
        %this.schedule(180000, "startEventManual");
    }

    function XmasEventTick::sendWarning(%this, %minutes) {
        %msg = "<spop><spush><color:c6935f>Xmas Event starting in " @ (%minutes == 0.5 ? "30 seconds" : %minutes @ " minute(s)") @ "!<spop>";
        LiFxUtility::messageAll(2480, %msg);
    }

    function XmasEventTick::setProcessTicks(%bool) {
        if (%bool) {
            %this = "XmasEventTick";
            %this.eventID = %this.schedule($RampartGames::XmasEvent::EventIntervalMs, onProcessTick);
        } else {
            cancel(XmasEventTick.eventID);
        }
    }

    function RampartGames_XmasEvent::startEvent() {
        echo("Xmas Event Started!");

        // Use global variables properly
        %numGifts = $RampartGames::XmasEvent::NumGifts;
        %durationMinutes = $RampartGames::XmasEvent::EventDurationMs / 60000;

        // Send message with colors and line break
        LiFxUtility::messageAll(2480, "<spop><spush><color:c6935f>Xmas Event has started!<spop><color:ffffff> Find the <color:c6935f>" @ %numGifts @ " gifts!<color:ffffff>\nYou have <color:c6935f>" @ %durationMinutes @ " minute(s)<color:ffffff> to loot all presents.<spop>");

        %this = "RampartGames_XmasEvent";

        for (%i = 0; %i < %numGifts; %i++) {
            %this.triggerObjectspawn(%i + 1);
        }

        // Schedule end of event
        %this.schedule($RampartGames::XmasEvent::EventDurationMs, "endEvent");
    }

    function RampartGames_XmasEvent::endEvent() {
        echo("Xmas Event Ended!");
        LiFxUtility::messageAll(2480, "<spop><spush><color:c6935f>Xmas Event has ended!<spop> All gifts removed.");
        RampartGames_XmasEvent::deleteAllFromFile();
    }

    function RampartGames_XmasEvent::triggerEventManually() {
        echo("Manual Xmas Event Triggered!");
        // Cancel any automated scheduling to prevent double triggers
        cancel(XmasEventTick.eventID);

        %this = "XmasEventTick";

        // Send warnings
        %this.sendWarning(3);                 // 3 min
        %this.schedule(120000, "sendWarning", 1);   // 1 min
        %this.schedule(150000, "sendWarning", 0.5); // 30 sec

        // Start event after 3 minutes
        %this.schedule(180000, "startEventManual");
    }

    function XmasEventTick::startEventManual() {
        RampartGames_XmasEvent::startEvent();
    }

    function RampartGames_XmasEvent::triggerObjectspawn(%this, %num) {
        %minX = -3060; %maxX = 3060;
        %minY = -3060; %maxY = 3060;

        while (!%position) {
            %x = getRandom(%minX, %maxX);
            %y = getRandom(%minY, %maxY);
            %position = isValidPosition(%x, %y);
        }

        %geoID = LiFxUtility::getGeoID(%position);
        %container = createTestMovable(4093, %geoID, 0, 2);

        if (!%container) {
            echo("Failed to spawn object with Geo ID:" SPC %geoID);
        } else {
            echo("Spawned container:" SPC %container);
            RampartGames_XmasEvent::recordSpawn(%container, %geoID, %position);
            %this.schedule($RampartGames::XmasEvent::EventDurationMs, "cleanupContainer", %container);
        }
    }

    function RampartGames_XmasEvent::recordSpawn(%this, %container, %geoID, %pos) {
        $RampartGames::XmasEvent::SpawnedList[$RampartGames::XmasEvent::SpawnedListCount] = %container;
        $RampartGames::XmasEvent::SpawnedListCount++;
        RampartGames_XmasEvent::saveSpawnList();
    }

    function RampartGames_XmasEvent::saveSpawnList() {
        %folder = $RampartGames::XmasEvent::DataPath @ "/XmasData";
        if (!isDirectory(%folder)) { createPath(%folder @ "/dummy.txt"); fileDelete(%folder @ "/dummy.txt"); }
        %path = %folder @ "/XmasObjects.cs";

        %file = new FileObject();
        if (%file.openForWrite(%path)) {
            for (%i = 0; %i < $RampartGames::XmasEvent::SpawnedListCount; %i++) {
                %geoID = $RampartGames::XmasEvent::SpawnedList[%i];
                %file.writeLine(%geoID);
            }
            %file.close();
        }
        %file.delete();
    }

    function RampartGames_XmasEvent::loadSpawnList() {
        %path = $RampartGames::XmasEvent::DataPath @ "/XmasData/XmasObjects.cs";
        %file = new FileObject();
        if (!%file.openForRead(%path)) { %file.delete(); return; }

        $RampartGames::XmasEvent::SpawnedListCount = 0;
        while (!%file.isEOF()) {
            %line = %file.readLine();
            if (%line $= "") continue;
            $RampartGames::XmasEvent::SpawnedList[$RampartGames::XmasEvent::SpawnedListCount] = %line;
            $RampartGames::XmasEvent::SpawnedListCount++;
        }
        %file.close();
        %file.delete();
    }

    function RampartGames_XmasEvent::deleteAllFromFile() {
        %folder = $RampartGames::XmasEvent::DataPath @ "/XmasData";
        %path = %folder @ "/XmasObjects.cs";
        %file = new FileObject();

        if (!%file.openForRead(%path)) { %file.delete(); return; }

        while (!%file.isEOF()) {
            %geoID = %file.readLine();
            if (%geoID $= "") continue;
            DeleteTestMovable(%geoID);
        }

        %file.close();
        %file.delete();

        %file = new FileObject();
        if (%file.openForWrite(%path)) %file.close();
        %file.delete();

        $RampartGames::XmasEvent::SpawnedListCount = 0;
    }

    function RampartGames_XmasEvent::cleanupSavedSpawns(%this) {
        for (%i = 0; %i < $RampartGames::XmasEvent::SpawnedListCount; %i++) {
            %geoID = $RampartGames_XmasEvent::SpawnedList[%i];
            if (%geoID !$= "") DeleteTestMovable(%geoID);
        }
        $RampartGames::XmasEvent::SpawnedListCount = 0;
        %this.schedule($RampartGames::XmasEvent::EventDurationMs, "deleteAllFromFile");
    }

    function RampartGames_XmasEvent::cleanupContainer(%this, %container) {
        if (!isObject(%container)) return;
        %container.deleteAllFromFile();
    }

    function isValidPosition(%x, %y) {
        %z = LiFxUtility::getTerrainHeightVector(%x);
        %rayStart = %x SPC %y SPC 1500;
        %rayEnd   = %x SPC %y SPC (%z - 100);
        %mask     = $TypeMasks::WaterObjectType;

        %result = containerRayCast(%rayStart, %rayEnd, %mask);
        if (%result) return false;
        return %x SPC %y SPC %z;
    }

};

activatePackage(RampartGames_XmasEvent);
RampartGames_XmasEvent::setup();

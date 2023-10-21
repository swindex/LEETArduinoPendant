
# LEETArduinoPendant
A UCCNC Plugin to receive commands from an Arduino Nano-powered CNC Pendant
Uses Solid.Arduino library to talk to Arduino Nano

## DevConsoleApp
- use this diagnistics app to check if all pins are properly detected

## The full list of components, pictures and videos used can be found here:
https://zero-divide.net/?article_id=5319_custom-cnc-pendant-project

## Arduino Setup
* Install the Full Firmata library onto your Arduino Nano

## UCCNC Setup
* Download the latest LEETArduinoPendant from the releases and extract it to your PC.
* Copy the `LEETArduinoPendant.dll` plugin file to the C:\UCCNC\Plugins directory!
* Copy the `Solid.Arduino.dll` file from the `Solid.Arduino` release folder to the C:\UCCNC directory.
* Please note that you might need to install `Arduino USB Driver` in order for your Arduino to work on the target computer!
* Launch UCCNC.exe, go to `Settings->Configure Plugins`, and mark the `LEET Arduino Pendant` plugin as `Enabled` and `Call startup`
* After everything is installed, Arduino is connected and UCCNC is running, press the "Enable" (+5v to D2-B-ENABLE ) button that will tell the plugin that the pendant is ready to send signals!
D13 (LED+) will then have a continuous +3.3V signal alerting you of that!

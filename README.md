# LEETArduinoPendant
A UCCNC Plugin to receive commands from an Arduino Nano-powered CNC Pendant
Uses Solid.Arduino library to talk to Arduino Nano

## The full list of components, pictures, and videos used can be found here:
https://zero-divide.net/?article_id=5319_custom-cnc-pendant-project

### 1. PCB Design
* Electronics Design: https://a360.co/3NGWRTP
* https://a360.co/3NLhfDk
* https://a360.co/3JObaVh
![Schematic](https://github.com/swindex/LEETArduinoPendant/assets/31745189/b3058bf5-83ec-4c52-a95d-344bfa370aed)
### Design notes:
* All resistors used were 10K
* Speed and Feed potentiometers are 10K
  Their pinout goes like this: Wiper->ground = 0%, Wiper -> +5V = 200%
* Axis and Handwheel Increment rotary switches are 4-position and their pinout goes like this:
  * Axis select pins: 1-X, 2-Y, 3-Z, 4-A, C-Common (+5V)
  * Handwheel Increment pins: 1-0.0001, 2-0.0010, 3-0.0100, 4-0.1000, C-Common (+5V)

* All function buttons such as Jog +/-, M1, M2, etc are sending +5V signals to the digital pins D2-D12
  D13 digital pin is connected to a LED indicator and is used to tell the user that the Pendant is allowed to send signals to the plugin.
  
## 2. Enclosure Design
https://a360.co/3fG3jgI
![image](https://github.com/swindex/LEETArduinoPendant/assets/31745189/6690877b-9217-4c35-8ea7-979f2510f035)

## 3. Arduino Setup
* Install the Full Firmata library onto your Arduino Nano (Or any other Ardiono that supports USB Serial Communication):
![image](https://github.com/swindex/LEETArduinoPendant/assets/31745189/823c5a27-9cb1-4e67-8edb-d3e4c108724c)

`StandardFirmata` sketch used for Arduino NANO is also added to the release package
* Please note that you might need to install `Arduino USB Driver` in order for your Arduino to work on the target computer!

## 4. UCCNC Setup
* Download the latest LEETArduinoPendant from the releases and extract it to your PC.
* Copy the `LEETArduinoPendant.dll` plugin file to the `C:\UCCNC\Plugins` directory!
* Copy the `Solid.Arduino.dll` file from the `Solid.Arduino` release folder to the `C:\UCCNC` directory.
* Please note that you might need to install `Arduino USB Driver` in order for your Arduino to work on the target computer!
* Launch UCCNC.exe, go to `Settings->Configure Plugins`, and mark the `LEET Arduino Pendant` plugin as `Enabled` and `Call startup`
* After everything is installed, Arduino is connected and UCCNC is running, press the "Enable" (+5v to D2-B-ENABLE ) button that will tell the plugin that the pendant is ready to send signals!
D13 (LED+) will then have a continuous +3.3V signal alerting you of that!


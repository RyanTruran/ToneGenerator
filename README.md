
# Tone Generator

Tone Generator is a MVVM WPF allpication that is used to generate sound on a Controller running **[ToneGenerator-Embedded](Tone)**. 


## Getting Started

To get started simply clone this repository, 
```git clone https://github.com/RyanTruran/ToneGenerator.git```
Then build and deploy the project in visual studio. 

Application was built using Visual Studio 2017 **.Net Framework 4.6.1**
and requires a minimum of .Net Framework 4.5.

## Using Tone Generator
![Tone Generator Main View](https://imgur.com/kHZj2Cn.jpg)

### Configure Settings



Configure the settings by selecting Connection in the menu bar then selecting Settings.

![Settings View](https://imgur.com/nKNbPv0.jpg)

Change the Port Name to the COM Port that the device has attached to. By Default, My computer connects to COM3 so i made that the default value. Feel free to change that in the connection constructor within SerialInterfaceView.cs to your own desired settings.

Baud Rate, Data Bits, Parity, Stop Bits, and Handshaking, are all configured as needed by the **ToneGenerator-Embedded** application. but can be modified if you change the settings in **ToneGenerator-Embedded**.

Settings will be saved as they are modified.

Close window once complete.
### Connect and Begin
After the settings have been configured, connect to the device by selecting Connection in the menu bar then clicking on connect. 

Once Connected, The Connection Status should be updated at the bottom of the window, and the buttons will now be enabled. 

Pressing one of the buttons will result in a tone being generated in tune with the note specified on the button. 

For instance, Pressing the 'A' Button on the window, will result in an 'A' Note being played by the **ToneGenerator-Embedded** Device.

Additionally, the Console will be updated to provide immediate feedback on which note was played, with the last message displaying on the top.

## Features that need to be implemented

Blocking the Execution of the connect button once the device is connected,

Killing all threads once the application exits. 


<!--stackedit_data:
eyJoaXN0b3J5IjpbLTI1MTI5NjkzOSwtOTUxODI5NzNdfQ==
-->

This is READEME_windows.txt which describes how to install Oscilloscope sw for windows platform.

Please download all contents of following

https://github.com/vijayandra/oscopev1/tree/master/Oscilloscope/windows_intel
save them some where in your folder where you can remember easily, 
currently it does not have installer.
Please follow following procedure according to your operating system.

WinXP 32/64 Bit
-------------------------
1. Turn Computer ON.
2. Login using your user name.
3. Press Windows key + R, when RUN window appear then type devmgmt.msc
4. Windows device manager should pop up.
5. Use USBMiniB cable to connect scope hardware.
6. Wait for few seconds, a message will show up on right corner saying new device has been found.
7. A New comport will appear in device manager under port section.
8. This may be port number (COM1,COM2.. COMX).

9. Go to directory where Oscilloscope files are saved.
10. start iwscope application, close it after few seconds.
11. A new file scope.ini generated at same time.
12. Now edit scope.ini file.Edit line for example COM6 type  
COMPORT=6 in scope.ini
if new port is COM47 then COMPORT=47

13. After entering correct port number start iwscope.exe application.
    Just do simple click on top left black corner (Plug symbol) Connect button.
14.Right bottom LED should turn green (grey when not connected).

15. Scope/Signal generator Ready to use.

Windows 7/Windows 10
----------------------------
As this driver now been certified with Microsoft WHQL 
http://answers.microsoft.com/en-us/insider/forum/insider_wintp-insider_devices/how-do-i-disable-driver-signature-enforcement-win/a53ec7ca-bdd3-4f39-a3af-3bd92336d248?auth=1

1. Please read above microsoft link, you need to start PC in safe more before you install iwscope.inf file.
2. Connect Oscilloscope via USB, browse to inf file when prompted.
3. Once driver installed, a new COM port will pop up in device section.
4. Every thing else is same as Windows XP, follow from step #8


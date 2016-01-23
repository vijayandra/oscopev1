
This is READEME_windows.txt which describes how to install Oscilloscope sw for linux platform.

Please download all contents of following

https://github.com/vijayandra/oscopev1/tree/master/Oscilloscope

save them some where in your folder where you can remember easily,
currently it does not have installer.
Please follow following procedure according to your operating system.

Linux 32/64 Bit
-------------------------
1. Turn Computer ON.
2. Login using your user name.
3. Go to command prompt, copy all files in directory.
4. Add "export LD_LIBRARY_PATH:." at end of your .bashrc file
   Or type "export LD_LIBRARY_PATH:." at bash shell.


5. Change directory where iwscope and libfixed_lib.so is located.
6. At bash shell command prompt type

  $ dmesg <hit enter>
  $ sudo dmesg -C
  (it clears kernel buffer)

7.Run following command, to give permission to user to read/write serial port

    $ sudo adduser $(USER) dialout
    $ sudo adduser $(USER) plugdev
    [sudo] password for xx:
    The user $USER is already a member of `dialout'.

    [ Settings do not take effect until reboot]

8. Now connect Oscilloscope USB board with mini USB cable.
  $ dmesg <hit enter>

    [10049.373336] usb 3-1: USB disconnect, device number 3
    [10051.364697] usb 3-1: new full-speed USB device number 4 using xhci_hcd
    [10051.408123] usb 3-1: New USB device found, idVendor=2429, idProduct=0035
    [10051.408135] usb 3-1: New USB device strings: Mfr=1, Product=2, SerialNumber=0
    [10051.408153] usb 3-1: Product: CDC Calibraion Device TST
    [10051.408158] usb 3-1: Manufacturer: IWSCOPE Inc Massachusetts
    [10051.411287] cdc_acm 3-1:1.0: This device cannot do calls on its own. It is not a modem.
    [10051.411352] cdc_acm 3-1:1.0: ttyACM0: USB ACM device

7. Make a note of ttyACM0 id,
8. Now edit scope.ini file.

    [SCOPE1]
    ACTIVE=1
    COMPORT=0
    TEMPERATURE_CHANNEL=0

    ttyACM0=>(COMPORT=0)
    ttyACM1=>(COMPORT=1)
    ttyACM2=>(COMPORT=2)

7. Now start iwscope application,

8. Just do simple click on top left black corner (Plug symbol) Connect button.

9.Right bottom LED should turn green (grey when not connected).

15. Scope/Signal generator Ready to use.


----------
Bootloader(Firmware Update Procedure)

copy following files to directory /etc/udev/rules.d
as bootloader works as HID device. this is mandatory
for linux platform for bootloader to work.

z010_mchp_tools.rules
z011_mchp_jlink.rules


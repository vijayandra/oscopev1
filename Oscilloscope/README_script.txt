
1. Please update to latest firmware.

How to update firmware.

1. Connect HW in normal Oscilloscope mode.
2. Bootloader button located next to LED on top bar.
3. Click on Bootloader button ( Located on first tab)
4. Three clicks needed to update firmware.

5. First click will show Status Msg << Starting Oscilloscope Bootloader Mode >>
6. Second Click          Status Msg << Oscilloscope Bootloader Mode>>
7. Third Click           Status Msg << Erasing Flash>>

8. <<Progress and Last Msg CRC Good Jumping to main >>

.. Firmware update successful
9. Close Oscilloscope Application and Disconnect USB Cable, and reconnect.
----------------------------------

Script Mode

1. Start application, connect to hardware.
2. Start a terminal program,

    $ run_myscript timechg_signal.c

    it will produce time changing signal.

    Incase if you are running this script on different computer where board is connected
    then correct _client.ini file to point host PC address.

    Main Oscilloscope application and Script can run on different computer or OS.
    they need to be connected on same network as they use TCP/IP pipe.

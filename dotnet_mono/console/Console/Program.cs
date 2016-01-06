using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdOut=System.Console;
using Setting;
using MyStopWatch;
using System.Collections;
using System.IO.Ports;
using System.IO;
using System.Threading;
//using System.Windows.Interop;
using scope_hw;

namespace Console
{
class Program
{
    private static System.Timers.Timer myTimer;

    static void Main(string[] args)
    {
        byte[] byteBuff    = new byte[256];
        byte[] cmdx        = new byte[1];
        byte[] len         = new byte[1];
        const byte  INIT_TAB = 0x00;
        byte   retVal;
        byte[] byteRdBuff  = new byte[256];
        byte[] byteRddBuff = new byte[256];
        int    count=256;

        string[] cmPort;
        int pos,pIndex=0;
        IniParser ini_file;
        string sComPort;
        int speed;
        //System.Timers.Timer myTimer = new System.Timers.Timer();
        StopWatch  sTimer;
        SerialQueue Q   = new SerialQueue();
      
        //IntPtr windowHandle = (new WindowInteropHelper(this)).Handle;
        //HwndSource src = HwndSource.FromHwnd(windowHandle);
        //src.AddHook(new HwndSourceHook(WndProc));

        // Starts library
        fixed_lib.lStart();

        for(int i=0; i<256; i++)
        {
            Q.PushRxByte((byte)i);
            byteBuff[i]=(byte)(i);
            //System.Console.WriteLine("bytes={0}\r\n",Q.PullRxByte());
        }

        // Initialize tag by passing first argument as zero
        fixed_lib.lPushTag(INIT_TAB,ref byteBuff,0);
        fixed_lib.lPushTag(0xf, ref byteBuff,0);
        fixed_lib.TransmitTag();
        fixed_lib.WaitTag(1000);

        do
        {
            retVal=fixed_lib.lPullTag(ref cmdx,ref len,ref byteBuff);
            if(retVal>0)
            {
                System.Console.WriteLine("cmdx={0} len={1}\r\n",cmdx[0],len[0]);
            }

        }while(retVal>0);
        //

        fixed_lib.lStop();

        return ;

        myTimer          = new System.Timers.Timer();
        myTimer.Elapsed += OnTimedEvent;
        myTimer.Interval = 10;
        myTimer.Enabled  = false;



        for(int i=0; i<256; i++)
        {
            Q.PushRxByte((byte)i);
            byteBuff[i]=(byte)(i+10);
            //System.Console.WriteLine("bytes={0}\r\n",Q.PullRxByte());
        }

        //System.Console.WriteLine("-----------------------------\r\n");
        //Q.PullRx(byteRddBuff,80);
        //System.Console.WriteLine(ByteArrayToHexString(byteRddBuff));
        //Serial_Comm.encodeATEMsg(100,ref byteBuff,10);
        //return;


        const string iniFileName= @"serial.ini";

        using (StreamWriter w = File.AppendText(iniFileName)) ;
        ini_file = new IniParser(iniFileName);

        sTimer=new StopWatch();
        sTimer.Start();

        ini_file.AddSetting("PORTSETTINGS", "COM","1");
        ini_file.AddSetting("PORTSETTINGS", "SPEED","115200");

        ini_file.SaveSettings();

        sComPort   = ini_file.GetSetting("PORTSETTINGS", "COM");

        System.Console.WriteLine(args.Length);

        cmPort = SerialPort.GetPortNames();
        if (cmPort.Length == 0)
        {
            System.Console.WriteLine("No Serial Port Found");
        }
        else
        {
            System.Console.Write("One COM PORT found on system");
            System.Console.Write("Serial Port={0}", cmPort[0]);
           
            myTimer.Start();
        }

        Thread.Sleep(10000);
        Thread.Sleep(10000);
        Thread.Sleep(10000);
        Thread.Sleep(10000);


        StdOut.WriteLine("This is Main program\r\n");
        StdOut.WriteLine("This is Main program\r\n");
        StdOut.WriteLine("This is Main program\r\n");
        StdOut.WriteLine("This is Main program\r\n");
        StdOut.WriteLine("This is Main program\r\n");

        sTimer.Stop();
        StdOut.WriteLine("StopWatch Timer Val={0}\r\n", sTimer.GetElapsedTime());

        ini_file.SaveSettings();
    }

    public static string ByteArrayToHexString(byte[] Bytes)
    {
        StringBuilder Result = new StringBuilder(Bytes.Length * 2);
        string HexAlphabet = "0123456789ABCDEF";

        foreach (byte B in Bytes)
        {
            Result.Append(HexAlphabet[(int)(B >> 4)]);
            Result.Append(HexAlphabet[(int)(B & 0xF)]);
        }

        return Result.ToString();
    }

    public static byte[] HexStringToByteArray(string Hex)
    {
        byte[] Bytes = new byte[Hex.Length / 2];
        int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
                                     0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                     0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
                                   };

        for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
        {
            Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                              HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
        }

        return Bytes;
    }

    private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
        byte[] Bytes = new byte[10];
        //System.Console.WriteLine("Timer {0}", e.SignalTime);
       
        //Serial_Comm.encodeATEMsg(0x1000, ref Bytes,0);
    }
}
}

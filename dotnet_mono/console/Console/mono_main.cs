using System;
using scope_hw;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

public class HelloWorld
{
    static public void Main ()
    {
        byte[] byteBuff  = new byte[256];
        byte cmdx        = 0;
        byte len         = 0;
        const byte  INIT_TAB = 0x00;
        int   retVal;
        SerialQueue Q      = new SerialQueue();

        //Console.WriteLine ("Hello Mono World {0}",fixed_lib.getpid());
        Console.WriteLine ("Hello Mono World");

        // Starts library
        fixed_lib.lStart();

        for(int i=0; i<256; i++)
        {
            Q.PushRxByte((byte)i);
            byteBuff[i]=(byte)(i);
            //System.Console.WriteLine("bytes={0}\r\n",Q.PullRxByte());
        }

        int size = Marshal.SizeOf(byteBuff[0]) * byteBuff.Length;
        IntPtr pnt = Marshal.AllocHGlobal(size);
        Marshal.Copy(byteBuff, 0, pnt, byteBuff.Length);

        // Initialize tag by passing first argument as zero
        fixed_lib.lPushTag(INIT_TAB,pnt,0);
        fixed_lib.lPushTag(0xf,pnt,10);
        fixed_lib.TransmitTag();
        fixed_lib.WaitTag(10);

        do
        {
            retVal=0;

            retVal = fixed_lib.lPullTag(pnt);
            cmdx   = (byte)retVal;

            if(retVal>0)
            {
                cmdx=(byte)retVal;
                len=(byte)(retVal>>8);
                if(cmdx>0)
                {
                    System.Console.WriteLine("cmdx=0x{0:X} len=0x{1:X}\r\n",cmdx,len);
                    Marshal.Copy(pnt,byteBuff,0,byteBuff.Length);

                    for(int i=0;i<len;i++)
                    {
                        System.Console.WriteLine("0x{0:X} ",byteBuff[i]);
                    }
                }
            }
            retVal=cmdx;

        }while(cmdx>0);

        fixed_lib.lStop();
        Marshal.FreeHGlobal(pnt);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace PortComm
{
public class Serial_Comm
{
    static SerialPort  _serialPort;
    static bool        _continue=false;
    static bool        isInitializedPort=false;

    static Thread      wRxTxThread;

    static             Mutex mut_r;
    static             Mutex mut_w;
    static             SerialQueue Q;

    static byte[]     _ate_incomming = new byte[512];
    static byte[]     _ate_outgoing  = new byte[512];
    static int       iState   = 0;
    static int       iIndex   = 0;
    static int       iCounter = 0;
    static int       cSum     = 0;
    static int       iLen     = 0;
    const  byte      BEG_FRAME= 0x7e;
    const  byte      END_FRAME= 0x7f;
    //if (mut.WaitOne(1000)) { mut.ReleaseMutex(); }

    public static int Encode_Serial_Cmd()
    {

        return 0;
    }

    public static int Decode_Serial_Cmd()
    {

        return 0;
    }

    public static bool InitThreadVars()
    {
        Q                 = new SerialQueue();
        _continue         = false;
        isInitializedPort = true;
        mut_r             = new Mutex();
        mut_w             = new Mutex();

        return true;
    }

    public static bool setSerial(string portName, int portSpeed)
    {
        _serialPort       = new SerialPort();

        //System.Console.WriteLine("Serial Port Name=");
        //System.Console.WriteLine(portName);
        //return true;

        _serialPort.PortName  = portName;
        _serialPort.BaudRate  = portSpeed;
        _serialPort.Parity    = Parity.None;
        _serialPort.DataBits  = 8;
        _serialPort.StopBits  = StopBits.One;
        _serialPort.Handshake = Handshake.None;

        _serialPort.ReadTimeout  = 1;
        _serialPort.WriteTimeout = 1;
        _serialPort.Open();

        if (_serialPort.IsOpen)
        {
            InitThreadVars();
            _continue = true;
            wRxTxThread = new Thread(work_function_task);
            wRxTxThread.Start();

            System.Console.WriteLine("Thread Initialized");
            while (!wRxTxThread.IsAlive);
            return true;
        }
        else
        {
            return false;
        }
    }




    static void work_function_task()
    {
        byte[] byteBuff = new byte[256];
        int offset = 0;
        int cVal;
        int NumBytes = 0;
        int count = 256;

        while (_continue)
        {
            try
            {
                // Read Port
                NumBytes = _serialPort.Read(byteBuff, offset, count);
                if (NumBytes > 0)
                {
                    if(mut_r.WaitOne())
                    {
                        for (int i = 0; i < NumBytes; i++)
                        {
                            System.Console.WriteLine("0x{0:X} ",byteBuff[i]);
                            Q.PushRxByte(byteBuff[i]);
                        }
                        mut_r.ReleaseMutex();
                    }
                }
            }
            catch (TimeoutException)
            {
                //System.Console.WriteLine("-----");
            }

            try
            {
                if(mut_w.WaitOne(1))
                {
                    cVal = Q.PullTxByte();
                    //Q.PushTxByte((byte)('C'));
                    //byteBuff[0]=(byte)'C';
                    //_serialPort.Write(byteBuff,0,1);
                    //System.Console.WriteLine("0x{0:X} ",byteBuff[0]);
                    //System.Console.WriteLine("-----");

                    if (cVal != 0x1000)
                    {
                        byteBuff[0] = (byte)cVal;
                        // Write Port
                        _serialPort.Write(byteBuff, 0, 1);
                        //System.Console.WriteLine("0x{0:X} ",byteBuff[0]);
                    }
                    mut_w.ReleaseMutex();
                }
            }
            catch (TimeoutException)
            {
                System.Console.WriteLine("-----");
            }
            //System.Console.WriteLine("-----");
        }
    }

    public static bool isCmdReady(ref byte[] arr)
    {
        int cVal;
        bool retVal=false;
        byte InputChar;

        cVal = Q.PullTxByte();
        while(cVal != 0x1000)
        {
            InputChar=(byte)cVal;
            //switch((byte)(0xff) & (byte)cVal)
            switch(iState)
            {
            case 0:
                iState=0;
                iIndex=0;
                _ate_incomming[iIndex++]=InputChar;        // Header
                if(BEG_FRAME==InputChar) iState=1;
                //printf("Header =0x%02x\n", InputChar);
                //return 1;

                break;
            case 1:
                _ate_incomming[iIndex++]=InputChar; // Len H
                iLen=(InputChar << 8);
                iState=2;
                //printf("Len H =0x%02x\n", InputChar);
                break;
            case 2:
                _ate_incomming[iIndex++]=InputChar; // Len L
                iLen=iLen | InputChar;
                iState=3;
                //printf("Len L =0x%02x %d\n", InputChar, iLen);
                break;
            case 3:
                _ate_incomming[iIndex++]=InputChar; // Cmdx H
                //printf("cmdx H =0x%02x\n", InputChar);
                iState=4;
                break;
            case 4:
                _ate_incomming[iIndex++]=InputChar; // Cmdx L
                //printf("cmdx L =0x%02x\n", InputChar);
                iState=5;
                break;
            case 5:
                _ate_incomming[iIndex++]=InputChar; // Sum1
                cSum=(_ate_incomming[1]+_ate_incomming[2]+_ate_incomming[3]+_ate_incomming[4]);

                if(cSum!=InputChar)
                {
                    iState=0;
                    //printf("Bad Message");
                }
                else
                {
                    iState=6;
                    //printf("Good Message");
                }
                cSum+=(_ate_incomming[1]+_ate_incomming[2]+_ate_incomming[3]+_ate_incomming[4]);
                //printf("checksumL =0x%02x\n", InputChar);
                //printf("Cmdx L =0x%02x\n", InputChar);
                iLen=iLen-5;
                if(iLen==0)
                {
                    iState=8;
                }
                else
                {
                    iState=7;
                }
                break;
            case 7:
                cSum+=InputChar;
                //printf("<Data Points=%d>", iLen);
                _ate_incomming[iIndex++]=InputChar;
                //printf("data 0x%02x\n", InputChar);
                if(iLen>0) iLen--;

                if(iLen>0)
                {
                    iState=7;
                }
                else
                {
                    iState=8;
                }

                break;
            case 8:
                iState=9;
                _ate_incomming[iIndex++]=InputChar; // CheckSum H
                //printf("CheckSum H 0x%02x\n", InputChar);
                break;
            case 9:
                iState=10;
                _ate_incomming[iIndex++]=InputChar; // CheckSum L
                //printf("CheckSum L 0x%02x\n", InputChar);
                //printf("CSUM=0x%04x\n",cSum);
                cSum=(_ate_incomming[1]+_ate_incomming[2]+_ate_incomming[3]+_ate_incomming[4]);
                break;
            case 10:
                _ate_incomming[iIndex++]=InputChar; // EndFrame
                iState=0;
                iIndex=0;

                if(InputChar==END_FRAME)
                {
                    return true;
                }
                //printf("End Frame 0x%02x\n", InputChar);
                //DebugATEMsg("Balle Balle\r\n");
                break;
            default:
                break;

            }
            cVal = Q.PullTxByte();
        }
        // Create the array on demand:
        //if (arr == null) { arr = new byte[10]; }
        // Fill the array:
        //arr[0] = 1111;
        //arr[4] = 5555;

        return false;
    }

    public static int encodeATEMsg(int cmdx, ref byte[] ucData,int len)
    {
        const int MIN_MSG_LEN = 5;

        int  j;
        int  iState=0;
        int  iIndex=0;
        int  iLen;
        int  cSum;
        int  tmpVar;


        iLen=MIN_MSG_LEN+len;

        _ate_outgoing[iIndex++]=BEG_FRAME;
        tmpVar=iLen;
        _ate_outgoing[iIndex++]=(byte)(iLen>>8);
        iLen=tmpVar;
        _ate_outgoing[iIndex++]=(byte)(iLen);
        tmpVar=cmdx;
        _ate_outgoing[iIndex++]=(byte)(cmdx>>8);
        cmdx=iLen;
        _ate_outgoing[iIndex++]=(byte)cmdx;
        _ate_outgoing[iIndex++]= (byte)(_ate_outgoing[1]+ _ate_outgoing[2]+ _ate_outgoing[3]+ _ate_outgoing[4]);
        cSum=(_ate_outgoing[1]+ _ate_outgoing[2]+ _ate_outgoing[3]+ _ate_outgoing[4]);
        cSum+=cSum;

        for(j=0; j<len; j++)
        {
            _ate_outgoing[iIndex++]=ucData[j];
            cSum+= ucData[j];
        }

        tmpVar=cSum;
        _ate_outgoing[iIndex++]= (byte)(cSum >>8);
        cSum=tmpVar;
        _ate_outgoing[iIndex++]= (byte)cSum;
        _ate_outgoing[iIndex++]=END_FRAME;

        for(int i=0; i<(len+9); i++)
        {
            //System.Console.Write("0x{0:X} ", _ate_outgoing[i]);
            System.Console.WriteLine("{0} 0x{1:X} ",i,  _ate_outgoing[i]);
            Q.PushTxByte(_ate_outgoing[i]);
        }

        return (len+9);
    }

    public static int writeOutBuffer(string sbuff)
    {
        if(mut_w.WaitOne())
        {
            // if(Uri.IsHexDigit(myString[i]))
            for(int i=0; i<sbuff.Length; i++)
            {
                Q.PushTxByte((byte)(sbuff[i]));
            }
            mut_w.ReleaseMutex();
        }
        return (sbuff.Length);
    }

}
}

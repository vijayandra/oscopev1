using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace scope_hw
{
enum Importance
{
    INIT_TAG=      0x00,
    TAG     =      0x01,
    I2C1START=     0x01,
    I2C1STOP=      0x02,
    I2C1RESTART=   0x03,
    I2C1WRITEADDR= 0x04,
    I2C1READ=      0x05,
    I2C1SPEED=     0x06,
    I2C1DATA=      0x35,
    I2C1READADDR=  0x37,
    I2C2START=     0x07,
    I2C2STOP=      0x08,
    I2C2RESTART=   0x09,
    I2C2WRITEADDR= 0x0a,
    I2C2READ=      0x0b,
    I2C2SPEED=     0x0c,
    I2C2DATA=      0x36,
    I2C2READADDR=  0x38,
    SET_PWM_RES=   0x0d,
    SET_PWM_VAL=   0x0e,
    UART1BAUD=     0x0f,
    UART1READ=     0x10,
    UART1WRITE=    0x11,
    UART2BAUD=     0x12,
    UART2READ=     0x13,
    UART2WRITE=    0x14,
    READ_ADC1=     0x15,
    READ_ADC2=     0x16,
    WRITE_DAC1=    0x17,
    WRITE_DAC2=    0x18,
    READ_GPIO_01=  0x19,
    READ_GPIO_02=  0x1a,
    READ_GPIO_03=  0x1b,
    READ_GPIO_04=  0x1c,
    READ_GPIO_05=  0x1d,
    READ_GPIO_06=  0x1e,
    READ_GPIO_07=  0x1f,
    READ_GPIO_08=  0x20,
    READ_GPIO_09=  0x21,
    READ_GPIO_10=  0x22,
    READ_GPIO_11=  0x23,
    READ_GPIO_12=  0x24,
    WRITE_GPIO_01= 0x25,
    WRITE_GPIO_02= 0x26,
    WRITE_GPIO_03= 0x27,
    WRITE_GPIO_04= 0x28,
    WRITE_GPIO_05= 0x29,
    WRITE_GPIO_06= 0x2a,
    WRITE_GPIO_07= 0x2b,
    WRITE_GPIO_08= 0x2c,
    WRITE_GPIO_09= 0x2d,
    WRITE_GPIO_10= 0x2e,
    WRITE_GPIO_11= 0x2f,
    WRITE_GPIO_12= 0x30,
    READ_FREQ1=    0x31,
    READ_FREQ2=    0x32,
    I2C1STATUS=    0x33,
    I2C2STATUS=    0x34,
    SIGCH1=        0x39,
    SIGCH2=        0x3A,
    SIGMIN=        0x3B,
    SIGMAX=        0x3C,
    SIGTYPE=       0x3D,
    SIGCNT=        0x3E,
    SIGSTART=      0x3F,
    GPIOINDEX=     0x40,
    DIR_IN=        0x41,
    DIR_OUT=       0x42,
    GPIO_READ=     0x43,
    GPIO_WRITE=    0x44,
    INCAP_RES=     0x45,
    INCAP_READ=    0x46,
    OUTCAP_RES=    0x47,
    OUTCAP_WRITE=  0x48,
    FREQ_RES=      0x49,
    FREQ_READ=     0x4A,
};


[StructLayout(LayoutKind.Explicit)]

struct sig
{
    [FieldOffset(0)]
    public short numFreq;
    [FieldOffset(2)]
    public short numPoint;
    [FieldOffset(4)]
    public short maxADAC;
    [FieldOffset(6)]
    public short minADAC;
    [FieldOffset(8)]
    public short maxBDAC;
    [FieldOffset(10)]
    public short minBDAC;
    [FieldOffset(12)]
    public short maxTmr;
    [FieldOffset(14)]
    public short maxTmrDiv;
    [FieldOffset(58)]
    public short lastMem;
}


[StructLayout(LayoutKind.Explicit)]
struct sco
{
    [FieldOffset(0)]
    public short actChnl;
    [FieldOffset(2)]
    public short numPoint;
    [FieldOffset(4)]
    public short numTmr;
    [FieldOffset(6)]
    public short numTmrDiv;
    [FieldOffset(8)]
    public short triggerSrc;
    [FieldOffset(10)]
    public short triggerType;
    [FieldOffset(12)]
    public short triggerVolt;
    [FieldOffset(58)]
    public short lastMem;
}

public static class fixed_lib
{
    [DllImport ("libfixed_lib.so")]
    public static extern int lStart();

    [DllImport ("libfixed_lib.so")]
    public static extern void lStop();

    [DllImport ("libfixed_lib.so")]
    public static extern void millisleep(int wTime);

    [DllImport ("libfixed_lib.so")]
    public static extern void TransmitTag();

    [DllImport ("libfixed_lib.so")]
    public static extern void WaitTag(int wTimeMs);

    [DllImport ("libfixed_lib.so")]
    public static extern byte lPushTag(byte cmd,IntPtr buffer,byte len);

    [DllImport ("libfixed_lib.so")]
    public static extern int lPullTag(IntPtr buffer);

    public static byte llPushTag(byte cmdx,ref byte[]byteBuff,byte len)
    {
        byte l;
        int size = Marshal.SizeOf(byteBuff[0]) * byteBuff.Length;
        IntPtr pnt = Marshal.AllocHGlobal(size);
        Marshal.Copy(byteBuff, 0, pnt, byteBuff.Length);

        l=lPushTag(cmdx,pnt,len);

        Marshal.FreeHGlobal(pnt);

        return l;
    }

    public static byte llPullTag(ref byte[]byteBuff,ref byte len)
    {
        byte cmdx;
        int retVal;
        int size = Marshal.SizeOf(byteBuff[0]) * byteBuff.Length;

        IntPtr pnt = Marshal.AllocHGlobal(size);

        retVal = lPullTag(pnt);

        cmdx = (byte)retVal;
        len  = (byte)(retVal>>8);

        Marshal.Copy(pnt,byteBuff,0,byteBuff.Length);

        Marshal.FreeHGlobal(pnt);

        return cmdx;
    }
}
}


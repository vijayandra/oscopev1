    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using Microsoft.Win32;
    using Microsoft.Win32.SafeHandles;
    using System.Runtime.InteropServices;

    namespace scope_hw
    {
    public static class fixed_lib
    {

    [StructLayout(LayoutKind.Sequential, Size = 23), Serializable]
    public struct header
    {
    // HEADER
    public UInt16 h_type;
    public UInt32 frame_num;
    public UInt16 count_1pps;
    public byte data_options;
    public byte project_type;
    public byte tile_num;
    public byte tile_set;
    public byte total_rows;
    public byte total_cols;
    public byte num_rows;
    public byte num_cols;
    public byte first_row;
    public byte first_col;
    public UInt16 num_sensors;
    public UInt16 num_data_bytes;
    public byte h_checksum;
    }

    [StructLayout(LayoutKind.Sequential, Size = 25), Serializable]
    public struct footer
    {
    // FOOTER
    public UInt16 f_type;
    public byte ts_len;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] ts_array;
    public byte frame_status;
    public byte f_checksum;
    }

    [StructLayout(LayoutKind.Sequential, Size = 51), Serializable]
    public struct buffer_node
    {
    // HEADER
    public header data_header;

    // DATA
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public byte[] data;

    // FOOTER
    public footer data_footer;
    }



    [StructLayout(LayoutKind.Sequential)]
    public struct MPLOT
    {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public double []    price;
    public int          streamSubset;
    }

    //[DllImport("dllname.dll", CallingConvention=CallingConvention.Cdecl)]
    //public static extern void cFunction(ref MPLOT mPlot);
 [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
            static extern IntPtr GetProcAddress( int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
            static extern bool FreeLibrary(int hModule);

    [DllImport("fixed_lib.dll")]
    public static extern int lStart();

    [DllImport("fixed_lib.dll")]
    public static extern void lStop();

    [DllImport("fixed_lib.dll")]
    public static extern void millisleep(int wTime);

    [DllImport("fixed_lib.dll")]
    public static extern void TransmitTag();

    [DllImport("fixed_lib.dll")]
    public static extern void WaitTag(int wTimeMs);

    [DllImport("fixed_lib.dll")]
    public static extern void lPushTag(byte cmd,ref byte[] buffer,byte len);

    [DllImport("fixed_lib.dll")]
    public static extern byte lPullTag(ref byte[] cmdx,ref byte[]len,ref byte[]buffer);
    }
    }


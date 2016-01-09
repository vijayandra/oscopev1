using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Windows.Interop;
using scope_hw;

namespace Oscope_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //private const int WM_DESTROY = 0x0002;
            InitializeComponent();

            IntPtr windowHandle = (new WindowInteropHelper(this)).Handle;
            HwndSource src = HwndSource.FromHwnd(windowHandle);
            src.AddHook(new HwndSourceHook(WndProc));
            scope_hw.fixed_lib.RegisterWin(windowHandle);
        }

        private void cSignal1_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            float dVal=0;
            int indexer;

            dVal = (float)(wParam.ToInt32())/1000.0f;
            indexer = (int)lParam;
            // Handle whatever Win32 message it is we feel like handling
            //if (lParam.ToInt32() == mTIME_Msg)
            //if(msg == mTIME_Msg)
                //this.status_msg.Text = "Indexer=" + indexer.ToString()+" Value=" + dVal.ToString();
                //this.status_msg.Text = "my_num....ToString()"+(iCounter++);
                //my_num++;
            //if (msg == WM_DESTROY)
            //{
                //IntPtr windowHandle = (new WindowInteropHelper(this)).Handle;
                //HwndSource src = HwndSource.FromHwnd(windowHandle);
                //src.RemoveHook(new HwndSourceHook(this.WndProc));
                //handled = true;
            //}
            return IntPtr.Zero;
        }
    }
}

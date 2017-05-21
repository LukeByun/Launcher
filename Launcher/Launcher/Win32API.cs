using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System.Security.Permissions;

namespace Launcher
{
    public class Win32API
    {
        public const Int32 WM_CREATE        = 0x0001;
        public const Int32 WM_DESTROY       = 0x0002;
        public const Int32 WM_CLOSE         = 0x0010;
        public const Int32 WM_QUIT          = 0x0012;
        public const Int32 WM_COMMAND       = 0x0111;
        public const Int32 WM_CONTEXTMENU   = 0x007B;
        public const Int32 WM_ACTIVATE      = 0x0006;
        public const Int32 WM_KEYDOWN       = 0x0100;
        public const Int32 WM_KEYUP         = 0x0101;

        public const Int32 WM_COPYDATA      = 0x004A;

        public const Int32 WM_LBUTTONDBLCLK = 0x0203;
        public const Int32 WM_LBUTTONDOWN = 0x0201;
        public const Int32 WM_LBUTTONUP = 0x0202;
        public const Int32 WM_MBUTTONDBLCLK = 0x0209;
        public const Int32 WM_MBUTTONDOWN = 0x0207;
        public const Int32 WM_MBUTTONUP = 0x0208;
        public const Int32 WM_MOUSEMOVE = 0x0200;
        public const Int32 WM_MOUSEWHEEL = 0x020A;
        public const Int32 WM_RBUTTONDBLCLK = 0x0206;
        public const Int32 WM_RBUTTONDOWN = 0x0204;
        public const Int32 WM_RBUTTONUP = 0x0205;

        internal const int SE_PRIVILEGE_ENABLED     = 0x00000002;
        internal const int TOKEN_QUERY              = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES  = 0x00000020;
        internal const string SE_SHUTDOWN_NAME      = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF               = 0x00000000;
        internal const int EWX_SHUTDOWN             = 0x00000001;
        internal const int EWX_REBOOT               = 0x00000002;
        internal const int EWX_FORCE                = 0x00000004;
        internal const int EWX_POWEROFF             = 0x00000008;
        internal const int EWX_FORCEIFHUNG          = 0x00000010;

        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public UInt32 cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref Win32API.COPYDATASTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hWnd, StringBuilder title, int size);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

//        protected override void WndProc(ref Message wMessage)
//        {

//            switch (wMessage.Msg)
//            {
//                case WM_COPYDATA:

//                    //Win32API.COPYDATASTRUCT lParam1 = (Win32API.COPYDATASTRUCT)Marshal.PtrToStructure(wMessage.LParam, typeof(Win32API.COPYDATASTRUCT));

//                    //Win32API.COPYDATASTRUCT lParam2 = new Win32API.COPYDATASTRUCT();

//                    //lParam2 = (Win32API.COPYDATASTRUCT)wMessage.GetLParam(lParam2.GetType());

//                    //MessageBox.Show("WM_COPYDATA : " + lParam1.lpData + "/ " + lParam2.lpData);
//                    break;
//                default:
//                    break;
//            }
////            base.WndProc(ref wMessage);
        //        }    

        #region 원도우 종료 및 리부팅 처리 함수
        /// <summary>
        /// 윈도우를 종료 하거나 리부팅 시킨다.
        /// </summary>
        /// <param name="flg">EWX_REBOOT, EWX_SHUTDOWN</param>
        public static void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }
        #endregion
    


    }
}

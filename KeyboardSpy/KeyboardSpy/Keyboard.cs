using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KeyboardSpy
{
    public class Keyboard
    {
        const int WH_KEYBOARD_LL = 13; // Номер глобального LowLevel-хука на клавиатуру

        private static IntPtr hhook = IntPtr.Zero;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private LowLevelKeyboardProc _proc = hookProc;

        public void SetHooks()
        {

            IntPtr hInstance = LoadLibrary("User32");
            IntPtr hMod = System.Runtime.InteropServices.Marshal.GetHINSTANCE(typeof(Keyboard).Module);
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hMod, 0);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            MessageBox.Show("dsfdfd");
            return CallNextHookEx(hhook, code, wParam, lParam);
        }

        public void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }


        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hInstance, uint threadId);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }
}
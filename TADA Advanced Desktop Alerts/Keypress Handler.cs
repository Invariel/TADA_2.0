using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// https://stackoverflow.com/questions/18291448/how-do-i-detect-keypress-while-not-focused
namespace TADA_Advanced_Desktop_Alerts
{
    public static class KeyPressConstants
    {
        // Windows message ID for "hotkey" https://msdn.microsoft.com/en-us/library/windows/desktop/ms646279(v=vs.85).aspx
        public const int WM_HOTKEY_MSG_ID = 0x0312;

        public const int MOD_ALT = 0x0001;      // Either Alt key.
        public const int MOD_CONTROL = 0x0002;  // Either Ctrl key.
        public const int MOD_SHIFT = 0x0004;    // Either Shift key.
        public const int MOD_WIN = 0x0008;      // Either Windows key.
    }

    public class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private int key;
        private int modifiers;
        private IntPtr hWnd;
        private int id;

        public KeyHandler(Keys key, List<Keys> modifiers, Form form)
        {
            this.key = (int)key;
            this.hWnd = form.Handle;
            this.modifiers = 0;

            if (modifiers.Contains<Keys>(Keys.Alt))
                this.modifiers += KeyPressConstants.MOD_ALT;

            if (modifiers.Contains<Keys>(Keys.Control))
                this.modifiers += KeyPressConstants.MOD_CONTROL;

            if (modifiers.Contains<Keys>(Keys.Shift))
                this.modifiers += KeyPressConstants.MOD_SHIFT;

            if (modifiers.Contains<Keys>(Keys.LWin) || modifiers.Contains<Keys>(Keys.RWin))
                this.modifiers += KeyPressConstants.MOD_WIN;

            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, modifiers, key);
        }

        public bool Unregister()
        {
            return UnregisterHotKey(hWnd, id);
        }

        // https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx is a full list of SendKeys keys.
        public static bool SendKeyToProgram(String program, String keystroke)
        {
            IntPtr windowHandler = FindWindow(null, program);
            if (windowHandler == IntPtr.Zero)
            {
                return false;
            }

            SetForegroundWindow(windowHandler);
            SendKeys.SendWait(keystroke);
            return true;
        }
    }
}
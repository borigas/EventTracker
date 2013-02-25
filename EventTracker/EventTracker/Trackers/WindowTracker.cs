using EventTracker.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.Trackers
{
    public class WindowTracker
    {
        static WinEventDelegate callback = new WinEventDelegate(WinEventProc);
        public static void Start()
        {
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, callback, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        public static void Stop()
        {

        }

        #region Win API Imports
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);
        #endregion

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private static Process GetActiveProcess()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            return GetProcess(handle);
        }

        public static Process GetProcess(IntPtr hwnd)
        {
            try
            {
                uint pid = 0;
                GetWindowThreadProcessId(hwnd, out pid);
                return Process.GetProcessById((int)pid); //Gets the process by ID. 
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            var activeProc = GetActiveProcess();
            using (var db = new EventTrackerContext())
            {
                db.WindowChanges.Add(new WindowChange()
                {
                    WindowName = activeProc.MainWindowTitle,
                    ProductName = activeProc.MainModule.FileVersionInfo.ProductName,
                    ModulePath = activeProc.MainModule.FileName,
                    ModuleName = Path.GetFileName(activeProc.MainModule.FileName),
                    EventTime = DateTime.Now,
                });
                db.SaveChanges();
            }
        }
    }
}

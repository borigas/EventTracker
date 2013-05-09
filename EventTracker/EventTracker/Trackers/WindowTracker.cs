using EventTracker.Helpers;
using EventTracker.Model;
using Microsoft.Win32;
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
    public class WindowTracker : BaseEventTracker
    {
        static WinEventDelegate callback = new WinEventDelegate(WinEventProc);
        public override void Start()
        {
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, callback, 0, 0, WINEVENT_OUTOFCONTEXT);

            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        public override void Stop()
        {
            //EventTrackerContext.Save(
            EventQueue.Enqueue(
                new WindowChange()
                {
                    WindowName = "App Shutdown",
                    ProductName = "App Shutdown",
                    ModulePath = "App Shutdown",
                    ModuleName = "App Shutdown",
                    EventTime = DateTime.Now,
                });

            SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
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

        public static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            //EventTrackerContext.Save(
            EventQueue.Enqueue(GetActiveWindowChange());
        }

        private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                //EventTrackerContext.Save(
                EventQueue.Enqueue(
                    new WindowChange()
                    {
                        WindowName = "Suspended",
                        ProductName = "Suspended",
                        ModulePath = "Suspended",
                        ModuleName = "Suspended",
                        EventTime = DateTime.Now,
                    });
            }
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLogoff ||
                e.Reason == SessionSwitchReason.SessionLock ||
                e.Reason == SessionSwitchReason.RemoteDisconnect)
            {
                //EventTrackerContext.Save(
                EventQueue.Enqueue(
                    new WindowChange()
                    {
                        WindowName = "Logged Off",
                        ProductName = "Logged Off",
                        ModulePath = "Logged Off",
                        ModuleName = "Logged Off",
                        EventTime = DateTime.Now,
                    });
            }
        }

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

        private static WindowChange GetActiveWindowChange()
        {
            string windowName = string.Empty;
            string productName = string.Empty;
            string modulePath = string.Empty;
            string moduleName = string.Empty;
            bool error = false;
            try
            {
                var activeProc = GetActiveProcess();
                if (activeProc != null && activeProc.MainModule != null && activeProc.MainModule.FileVersionInfo != null)
                {
                    windowName = activeProc.MainWindowTitle;
                    modulePath = activeProc.MainModule.FileName;
                    moduleName = Path.GetFileName(activeProc.MainModule.FileName);
                    productName = activeProc.MainModule.FileVersionInfo.ProductName;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                error = true;
            }

            return new WindowChange()
            {
                EventTime = DateTime.Now,
                WindowName = windowName,
                ModulePath = modulePath,
                ModuleName = moduleName,
                ProductName = productName,
                ErrorFetchingInfo = error,
            };
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
    }
}

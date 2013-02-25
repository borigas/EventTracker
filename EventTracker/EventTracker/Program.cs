using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventTracker;
using System.Drawing;
using EventTracker.Trackers;

namespace EventTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            CreateTrayIcon();

            KeyTracker.Start();
            WindowTracker.Start();

            Application.Run();

            WindowTracker.Stop();
            KeyTracker.Stop();
        }

        private static NotifyIcon trayIcon;
        private static ContextMenu trayMenu;

        private static void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            trayMenu.Dispose();

            Application.Exit();
        }

        private static void CreateTrayIcon()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Event Tracker";

            //trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(trayIcon_MouseClick);

            trayIcon.Icon = Icon.FromHandle(Properties.Resources.trayicon_success.GetHicon());
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }
    }
}

using EventTracker.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.Trackers
{
    public class LoggedOnTracker : BaseEventTracker
    {
        public override void Start()
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            // Must be logged on to start
            EventTrackerContext.Save(new LogOnEvent()
            {
                IsLoggedOn = true,
                EventTime = DateTime.Now,
            });
        }

        public override void Stop()
        {
            // Treat stop as a log off
            EventTrackerContext.Save(new LogOnEvent()
            {
                IsLoggedOn = false,
                EventTime = DateTime.Now,
            });

            SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLogoff ||
                e.Reason == SessionSwitchReason.SessionLock ||
                e.Reason == SessionSwitchReason.RemoteDisconnect)
            {
                EventTrackerContext.Save(new LogOnEvent()
                {
                    IsLoggedOn = false,
                    EventTime = DateTime.Now,
                });
            }
            else
            {
                EventTrackerContext.Save(new LogOnEvent()
                {
                    IsLoggedOn = true,
                    EventTime = DateTime.Now,
                });
            }
        }

        private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                EventTrackerContext.Save(new LogOnEvent()
                {
                    IsLoggedOn = false,
                    EventTime = DateTime.Now,
                });
            }
            else if (e.Mode == PowerModes.Resume)
            {
                EventTrackerContext.Save(new LogOnEvent()
                {
                    IsLoggedOn = true,
                    EventTime = DateTime.Now,
                });
            }
        }
    }
}

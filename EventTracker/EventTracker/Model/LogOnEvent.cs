using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.Model
{
    public class LogOnEvent : TrackableEvent
    {
        public long Id { get; set; }
        public bool IsLoggedOn { get; set; }
        public DateTime EventTime { get; set; }
    }
}

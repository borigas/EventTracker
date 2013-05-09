using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.Model
{
    public class WindowChange : TrackableEvent
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string WindowName { get; set; }
        public string ModuleName { get; set; }
        public string ModulePath { get; set; }
        public DateTime EventTime { get; set; }
        public bool ErrorFetchingInfo { get; set; }
    }
}

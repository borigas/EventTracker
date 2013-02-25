using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.Model
{
    public class WindowChange
    {
        public long Id { get; set; }
        public string WindowName { get; set; }
        public DateTime EventTime { get; set; }
    }
}

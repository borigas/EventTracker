using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTracker.Model
{
    public class KeyStroke
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string KeyCharacter { get; set; }
        public DateTime EventTime { get; set; }
    }
}

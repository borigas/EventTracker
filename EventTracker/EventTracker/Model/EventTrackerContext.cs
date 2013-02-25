using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EventTracker.Model
{
    public class EventTrackerContext : DbContext
    {
        public EventTrackerContext()
            : base("EventTracker")
        {
        }

        public DbSet<KeyStroke> KeyStrokes { get; set; }
        public DbSet<WindowChange> WindowChanges { get; set; }
    }
}

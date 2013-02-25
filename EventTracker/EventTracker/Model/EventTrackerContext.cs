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
            Console.WriteLine(this.Database.Connection.ConnectionString);
        }

        public DbSet<KeyStroke> KeyStrokes { get; set; }
        public DbSet<WindowChange> WindowChanges { get; set; }
    }
}

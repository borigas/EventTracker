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

        public static void Save<T>(T entity) where T : class
        {
            using (var db = new EventTrackerContext())
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            }
        }

        public DbSet<KeyStroke> KeyStrokes { get; set; }
        public DbSet<WindowChange> WindowChanges { get; set; }
        public DbSet<LogOnEvent> LogOnEvents { get; set; }
    }
}

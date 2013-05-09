using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EventTracker.Model
{
    public static class EventQueue
    {
        private static ConcurrentQueue<TrackableEvent> _queue = new ConcurrentQueue<TrackableEvent>();
        private static Timer _timer = null;

        public static void Enqueue<T>(T entity) where T : TrackableEvent
        {
            if (_timer == null)
            {
                _timer = new Timer(1000);
                _timer.Elapsed += _timer_Elapsed;
                _timer.AutoReset = true;
                _timer.Start();
            }
            _queue.Enqueue(entity);
        }

        public static void FlushQueue()
        {
            TrackableEvent entity;
 	        while(_queue.TryDequeue(out entity))
            {
                EventTrackerContext.Save(entity);
            }
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            FlushQueue();
        }
    }
}

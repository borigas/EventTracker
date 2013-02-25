using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.Trackers
{
    public abstract class BaseEventTracker : IDisposable
    {
        public abstract void Start();
        public abstract void Stop();

        public void Dispose()
        {
            Stop();
        }
    }
}

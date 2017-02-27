using System.Collections.Generic;
using Turbo.Events.Model;

namespace Turbo.Events.Reports.EventSequence
{
    public class Fatty : IConsumer, IOnError, IOnEvent, IOnScopedEvent
    {
        private readonly IList<object> _events;

        public Fatty(IList<object> events)
        {
            _events = events;
        }

        public void On(ErrorEvent e)
        {
        }

        public void On(Event e)
        {
            _events.Add(e);
        }

        public void On(ScopedEvent e)
        {
        }

        public void Dispose()
        {
        }
    }
}
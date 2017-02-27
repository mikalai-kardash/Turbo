using System.Collections.Generic;
using Turbo.Events.Model;

namespace Turbo.Events
{
    public class EventPipe : IEventer
    {
        private readonly List<IOnError> _errorHandler = new List<IOnError>();
        private readonly List<IOnScopedEvent> _scopeHandler = new List<IOnScopedEvent>();
        private readonly List<IOnEvent> _eventHandler = new List<IOnEvent>();

        public EventPipe(string source, IEventSystem factory)
        {
            Source = source;

            var consumers = factory.CreateConsumers(Source);
            if (consumers == null)
            {
                return;
            }

            foreach (var consumer in consumers)
            {
                _errorHandler.AddIf(consumer);
                _eventHandler.AddIf(consumer);
                _scopeHandler.AddIf(consumer);
            }
        }

        public string Source { get; }

        public void Report(Event evt)
        {
            foreach (var handler in _eventHandler)
            {
                handler.On(evt);
            }
        }
    }
}
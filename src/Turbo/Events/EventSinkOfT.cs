using Turbo.Events.Model;

namespace Turbo.Events
{
    public class EventSink<TSource> : IEventer<TSource>
    {
        private readonly EventPipe _pipe;

        public EventSink(IPipeFactory factory)
        {
            _pipe = factory.CreatePipe(typeof(TSource).FullName);
        }

        public string Source => typeof(TSource).FullName;

        public void Report(Event evt)
        {
            _pipe.Report(evt);
        }
    }
}
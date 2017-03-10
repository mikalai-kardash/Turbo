using Turbo.DI;
using Turbo.Events;

namespace Turbo
{
    public class EventsModule : Module
    {
        public EventsModule()
        {
            Registry.AddType(typeof(IEventer<>), typeof(EventSink<>));

            Registry
                .AddType<IEventSystem, EventSystem>()
                .WithAlias<IPipeFactory>();
        }
    }
}
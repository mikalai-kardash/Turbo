using System;
using Turbo.Events.Message;
using Turbo.Events.Model;

namespace Turbo.Events
{
    public static class EventerExtensions
    {
        public static void Info(this IEventer eventer, int partialId, string message, params object[] args)
        {
            eventer.Report(new Event(partialId, eventer.Source)
            {
                Message = new TemplatedString(message, args)
            });
        }

        public static IDisposable Measure(this IEventer eventer, int partialId, string message, params object[] args)
        {
            var scope = Guid.NewGuid();
            return new Scope(eventer, partialId, eventer.Source, scope, message, args);
        }
    }
}
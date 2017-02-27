using System;

namespace Turbo.Events.Model
{
    public class ErrorEvent : Event
    {
        public ErrorEvent(int partialId, string source) : base(partialId, source)
        {
        }

        public Exception Exception { get; set; }
    }
}
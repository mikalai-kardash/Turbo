using System;

namespace Turbo.Events.Model
{
    public class ScopedEvent : Event
    {
        public ScopedEvent(int partialId, string source, Guid scopeId)
            : base(partialId, source)
        {
            ScopeId = scopeId;
        }

        public Guid ScopeId { get; }

        public long Timestamp { get; set; }
    }
}
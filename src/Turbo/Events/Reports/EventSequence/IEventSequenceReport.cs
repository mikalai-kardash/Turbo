using System.Collections.Generic;

namespace Turbo.Events.Reports.EventSequence
{
    public interface IEventSequenceReport
    {
        IEnumerable<object> Events { get; }
    }
}
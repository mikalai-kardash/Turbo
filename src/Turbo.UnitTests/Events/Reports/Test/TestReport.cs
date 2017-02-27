using System.Collections.Generic;
using Turbo.Events;
using Turbo.Events.Model;
using Turbo.Events.Reports;

namespace Turbo.UnitTests.Events.Reports.Test
{
    public class TestReport : IReport
    {
        public List<Event> Events { get; } = new List<Event>();

        public void Dispose()
        {
        }

        public IConsumer CreateConsumer(string source)
        {
            return new TestConsumer(this);
        }

        public void Reset()
        {
        }

        public void Render(IReportView view)
        {
        }

        public void AddEvent(Event e)
        {
            Events.Add(e);
        }
    }
}
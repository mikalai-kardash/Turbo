using Turbo.Events;
using Turbo.Events.Model;

namespace Turbo.UnitTests.Events.Reports.Test
{
    public class TestConsumer : IConsumer, IOnEvent, IOnError, IOnScopedEvent
    {
        private readonly TestReport _report;

        public TestConsumer(TestReport report)
        {
            _report = report;
        }

        public void Dispose()
        {
        }

        public void On(Event e)
        {
            _report.AddEvent(e);
        }

        public void On(ErrorEvent e)
        {
        }

        public void On(ScopedEvent e)
        {
        }
    }
}
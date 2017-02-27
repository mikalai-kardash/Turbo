using System.Collections.Generic;

namespace Turbo.Events.Reports.EventSequence
{
    public class EventSequenceReport : SimpleReport<Fatty>, IEventSequenceReport
    {
        private readonly IList<object> _events = new List<object>();

        protected override Fatty CreateConsumerInstance(string source)
        {
            return new Fatty(_events);
        }

        public override void Reset()
        {
        }

        public override void Render(IReportView view)
        {
            var v = view as ICanRenderEventSequenceReport;
            v?.Render(this);
        }

        public IEnumerable<object> Events => _events;
    }
}
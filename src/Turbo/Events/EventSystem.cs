using System.Collections.Generic;
using System.Linq;
using Turbo.Events.Reports;

namespace Turbo.Events
{
    public class EventSystem : IEventSystem, IPipeFactory
    {
        private readonly IList<IReport> _reports = new List<IReport>();

        public IConsumer[] CreateConsumers(string source)
        {
            return _reports
                .Select(r => r.CreateConsumer(source))
                .Where(c => c != null)
                .ToArray();
        }

        public void AddReport(IReport report)
        {
            _reports.Add(report);
        }

        public EventPipe CreatePipe(string name)
        {
            return new EventPipe(name, this);
        }

        public void Report()
        {
        }

        public void Reset()
        {
        }
    }
}
using System;
using System.Collections.Concurrent;

namespace Turbo.Events.Reports
{
    public abstract class SimpleReport<T> : IReport
        where T : IConsumer
    {
        private readonly ConcurrentDictionary<string, IConsumer> _consumers
            = new ConcurrentDictionary<string, IConsumer>(StringComparer.Ordinal);

        public void Dispose()
        {
        }

        protected abstract T CreateConsumerInstance(string source);

        public IConsumer CreateConsumer(string source)
        {
            return _consumers.GetOrAdd(source, s => CreateConsumerInstance(s));
        }

        public abstract void Reset();

        public abstract void Render(IReportView view);
    }
}
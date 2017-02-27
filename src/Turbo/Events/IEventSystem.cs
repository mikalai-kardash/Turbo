using Turbo.Events.Reports;

namespace Turbo.Events
{
    public interface IEventSystem
    {
        IConsumer[] CreateConsumers(string source);

        void AddReport(IReport report);
    }
}
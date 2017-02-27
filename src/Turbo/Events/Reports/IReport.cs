namespace Turbo.Events.Reports
{
    public interface IReport : IConsumerFactory
    {
        void Reset();
        void Render(IReportView view);
    }
}
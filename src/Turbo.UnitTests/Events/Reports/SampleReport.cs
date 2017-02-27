using Turbo.Events.Reports;

namespace Turbo.UnitTests.Events.Reports
{
    internal class SampleReport : SimpleReport<SampleConsumer>
    {
        protected override SampleConsumer CreateConsumerInstance(string source)
        {
            return new SampleConsumer();
        }

        public override void Reset()
        {
        }

        public override void Render(IReportView view)
        {
        }
    }
}
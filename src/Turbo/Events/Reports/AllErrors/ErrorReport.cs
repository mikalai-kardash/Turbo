using System.Collections.Generic;
using Turbo.Events.Model;

namespace Turbo.Events.Reports.AllErrors
{
    public class ErrorReport : SimpleReport<ErrorConsumer>, IAllErrorsReport
    {
        private readonly List<ErrorEvent> _errors = new List<ErrorEvent>();

        public IEnumerable<ErrorEvent> Errors => _errors;

        protected override ErrorConsumer CreateConsumerInstance(string source)
        {
            return new ErrorConsumer(_errors);
        }

        public override void Reset()
        {
        }

        public override void Render(IReportView view)
        {
            var v = view as ICanRenderAllErrorsReport;
            v?.Render(this);
        }
    }
}
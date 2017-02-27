using System.Collections.Generic;
using Turbo.Events.Model;

namespace Turbo.Events.Reports.AllErrors
{
    public class ErrorConsumer : IConsumer, IOnError
    {
        private readonly List<ErrorEvent> _errors;

        public ErrorConsumer(List<ErrorEvent> errors)
        {
            _errors = errors;
        }

        public void On(ErrorEvent e)
        {
            _errors.Add(e);
        }

        public void Dispose()
        {
        }
    }
}
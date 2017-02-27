using System.Collections.Generic;
using Turbo.Events.Model;

namespace Turbo.Events.Reports.AllErrors
{
    public interface IAllErrorsReport
    {
        IEnumerable<ErrorEvent> Errors { get; }
    }
}
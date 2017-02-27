namespace Turbo.Events.Reports.AllErrors
{
    public interface ICanRenderAllErrorsReport
    {
        void Render(IAllErrorsReport report);
    }
}
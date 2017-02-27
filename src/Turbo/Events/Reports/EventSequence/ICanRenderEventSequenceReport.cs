namespace Turbo.Events.Reports.EventSequence
{
    public interface ICanRenderEventSequenceReport
    {
        void Render(IEventSequenceReport report);
    }
}
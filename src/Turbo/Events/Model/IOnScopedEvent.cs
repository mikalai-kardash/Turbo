namespace Turbo.Events.Model
{
    public interface IOnScopedEvent
    {
        void On(ScopedEvent e);
    }
}
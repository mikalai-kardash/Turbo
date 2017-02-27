namespace Turbo.Events.Model
{
    public interface IOnEvent
    {
        void On(Event e);
    }
}
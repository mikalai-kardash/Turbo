namespace Turbo.Events.Model
{
    public interface IOnError
    {
        void On(ErrorEvent e);
    }
}
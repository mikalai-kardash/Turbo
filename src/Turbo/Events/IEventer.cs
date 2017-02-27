using Turbo.Events.Model;

namespace Turbo.Events
{
    public interface IEventer
    {
        string Source { get; }

        void Report(Event etv);
    }
}
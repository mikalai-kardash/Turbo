using Turbo.Events.Message;

namespace Turbo.Events.Model
{
    public class Event
    {
        public Event(int partialId, string source)
        {
            PartialId = partialId;
            Source = source;
        }

        public int PartialId { get; }
        public string Source { get; }
        public TemplatedString Message { get; set; }
    }
}
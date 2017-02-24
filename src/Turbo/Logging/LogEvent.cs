using System;

namespace Turbo.Logging
{
    public class LogEvent
    {
        public int Id { get; set; }
        public string Template { get; set; }
        public string Message { get; set; }
        public Type Category { get; set; }
    }

    public class LogException : LogEvent
    {
        public Exception Exception { get; set; }
    }
}
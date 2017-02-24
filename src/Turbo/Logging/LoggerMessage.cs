using System;

namespace Turbo.Logging
{
    public static class LoggerMessage
    {
        public static Func<ILogger, IDisposable> DefineScope(string message)
        {
            var state = new LoggerState(message);
            return logger => logger.BeginScope(state);
        }

        public static Func<ILogger, T, IDisposable> DefineScope<T>(string message)
        {
            return (logger, arg) =>
            {
                var state = new LoggerState(message, arg);
                return logger.BeginScope(state);
            };
        }
    }
}
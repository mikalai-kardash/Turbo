using System;

namespace Turbo.Logging
{
    public interface ILogger
    {
        bool IsEnabled(LogLevel logLevel);

        void Log<TState>(
            LogLevel logLevel, 
            int eventId, 
            TState state, 
            Exception exception,
            Func<TState, Exception, string> formatter);

        IDisposable BeginScope<TState>(TState state);
    }
}
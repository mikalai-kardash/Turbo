using System;

namespace Turbo.Logging
{
    public class Logger<T> : ILogger<T>
    {
        private readonly ILogger _logger;

        public Logger(ILoggingFactory factory)
        {
            _logger = factory.CreateLogger(GetType().Name);
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        void ILogger.Log<TState>(LogLevel logLevel, int eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }
    }
}
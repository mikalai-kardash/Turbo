using System;

namespace Turbo.Logging
{
    public interface ILoggingFactory : IDisposable
    {
        ILogger CreateLogger(string name);
    }
}
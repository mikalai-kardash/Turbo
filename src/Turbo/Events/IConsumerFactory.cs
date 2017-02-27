using System;

namespace Turbo.Events
{
    public interface IConsumerFactory : IDisposable
    {
        IConsumer CreateConsumer(string source);
    }
}
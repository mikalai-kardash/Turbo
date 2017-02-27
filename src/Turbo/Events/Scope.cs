using System;
using Turbo.Events.Message;
using Turbo.Events.Model;

namespace Turbo.Events
{
    public class Scope : IDisposable
    {
        private static readonly DateTime Y1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private readonly IEventer _eventer;
        private readonly int _partialId;
        private readonly string _source;
        private readonly Guid _scope;
        private readonly string _template;
        private readonly object[] _args;

        private bool _disposed;

        public Scope(IEventer eventer, int partialId, string source, Guid scope, string template, object[] args)
        {
            _eventer = eventer;
            _partialId = partialId;
            _source = source;
            _scope = scope;
            _template = template;
            _args = args;

            _eventer.Report(GetEvent());
        }

        private ScopedEvent GetEvent()
        {
            return new ScopedEvent(_partialId, _source, _scope)
            {
                Message = new TemplatedString(_template, _args),
                Timestamp = GetTimestamp()
            };
        }

        private static long GetTimestamp()
        {
            return (long) DateTime.Now
                .ToUniversalTime()
                .Subtract(Y1970)
                .TotalMilliseconds;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (!disposing)
            {
                return;
            }
            _eventer.Report(GetEvent());
            _disposed = true;
        }
    }
}
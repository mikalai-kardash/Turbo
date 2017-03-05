using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Turbo.DI
{
    public class ObjectCache : IObjectCache
    {
        private bool _disposed;

        private readonly ConcurrentDictionary<TypeId, object> _objects
            = new ConcurrentDictionary<TypeId, object>();

        public void Add(TypeId id, object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            _objects.AddOrUpdate(id, k => instance, (k, v) => instance);
        }

        public IEnumerable<TypeId> AllObjectIds => _objects.Keys;

        public object Get(TypeId id)
        {
            object instance;
            return _objects.TryGetValue(id, out instance) ? instance : null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!disposing) return;

            _objects.Clear();
            _disposed = true;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Turbo.DI
{
    public interface IObjectCache : IDisposable
    {
        IEnumerable<TypeId> AllObjectIds { get; }

        void Add(TypeId id, object instance);
        object Get(TypeId id);
    }
}
using System;
using System.Collections.Generic;

namespace Turbo.DI
{
    public interface IObjectFactory : IDisposable
    {
        object GetInstance(Type type, string name);
        IEnumerable<TypeId> AllInstanceIds { get; }
    }
}
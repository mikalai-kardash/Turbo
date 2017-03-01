using System;

namespace Turbo.DI
{
    public interface IObjectFactory : IDisposable
    {
        object GetInstance(Type type, string name);
    }
}
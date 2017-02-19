using System;

namespace Turbo.DI
{
    public interface IObjectFactory
    {
        T GetInstance<T>();
        object GetInstance(Type type);
    }
}
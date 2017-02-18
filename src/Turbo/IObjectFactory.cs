using System;

namespace Turbo
{
    public interface IObjectFactory
    {
        T GetInstance<T>();
        object GetInstance(Type type);
    }
}
using System;

namespace Turbo.DI
{
    public interface IObjectFactory
    {
        object GetInstance(Type type);
    }
}
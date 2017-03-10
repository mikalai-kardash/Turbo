using System;

namespace Turbo.DI
{
    public interface IObjectFactoryRegistry : IDisposable
    {
        Registration RegisterType(Type from, Type to, string name);

        void RegisterInstance(Type type, object instance, string name);
    }
}
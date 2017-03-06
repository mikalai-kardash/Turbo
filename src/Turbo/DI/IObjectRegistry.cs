using System;

namespace Turbo.DI
{
    internal interface IObjectRegistry : IDisposable
    {
        Registration RegisterType(Type from, Type to, string name);

        void RegisterInstance(Type type, object instance, string name);
    }
}
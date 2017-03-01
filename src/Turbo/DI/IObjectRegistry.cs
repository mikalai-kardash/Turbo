using System;

namespace Turbo.DI
{
    internal interface IObjectRegistry : IDisposable
    {
        Registration[] Registrations { get; }

        Registration RegisterType(Type from, Type to);

        void RegisterInstance(Type type, object instance);
    }
}
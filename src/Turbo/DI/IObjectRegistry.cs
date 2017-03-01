using System;

namespace Turbo.DI
{
    internal interface IObjectRegistry
    {
        Registration RegisterType(Type from, Type to);

        void RegisterInstance(Type type, object instance);
    }
}
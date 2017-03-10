using System;

namespace Turbo.DI
{
    internal static class ObjectRegistryExtensions
    {
        public static Registration AddType<TFrom, TTo>(this IObjectFactoryRegistry registry)
        {
            return registry.RegisterType(typeof(TFrom), typeof(TTo), string.Empty);
        }

        public static Registration AddType(this IObjectFactoryRegistry registry, Type from, Type to)
        {
            return registry.RegisterType(from, to, string.Empty);
        }

        public static void Instance<T>(this IObjectFactoryRegistry registry, T instance)
        {
            registry.RegisterInstance(typeof(T), instance, string.Empty);
        }

        public static void InstanceOfObjectFactory(this IObjectFactoryRegistry registry, IObjectFactory factory)
        {
            registry.Instance(factory);
        }

        public static void InstanceOfObjectRegistry(this IObjectFactoryRegistry registry)
        {
            registry.Instance(registry);
        }
    }
}
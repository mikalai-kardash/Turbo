using System;

namespace Turbo.DI
{
    internal static class ObjectRegistryExtensions
    {
        public static Registration AddType<TFrom, TTo>(this IObjectRegistry registry)
        {
            return registry.RegisterType(typeof(TFrom), typeof(TTo), string.Empty);
        }

        public static Registration AddType(this IObjectRegistry registry, Type from, Type to)
        {
            return registry.RegisterType(from, to, string.Empty);
        }

        public static void Instance<T>(this IObjectRegistry registry, T instance)
        {
            registry.RegisterInstance(typeof(T), instance, string.Empty);
        }
    }
}
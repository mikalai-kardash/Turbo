namespace Turbo.DI
{
    internal static class ObjectRegistryExtensions
    {
        public static Registration AddType<TFrom, TTo>(this IObjectRegistry registry)
        {
            return registry.RegisterType(typeof(TFrom), typeof(TTo));
        }

        public static void Instance<T>(this IObjectRegistry registry, T instance)
        {
            registry.RegisterInstance(typeof(T), instance);
        }
    }
}
using System;

namespace Turbo.DI
{
    public static class ObjectFactoryExtensions
    {
        public static T GetInstance<T>(this IObjectFactory factory)
        {
            return (T) factory.GetInstance(typeof(T), string.Empty);
        }

        public static object GetInstance(this IObjectFactory factory, Type type)
        {
            return factory.GetInstance(type, string.Empty);
        }
    }
}
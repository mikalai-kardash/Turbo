namespace Turbo.DI
{
    public static class ObjectFactoryExtensions
    {
        public static T GetInstance<T>(this IObjectFactory factory)
        {
            return (T) factory.GetInstance(typeof(T));
        }
    }
}
namespace Turbo.DI
{
    internal static class InternalTypeIds
    {
        public static TypeId DefaultObjectFactory { get; } = TypeId.Unnamed(typeof(DefaultObjectFactory));
        public static TypeId ObjectRegistry { get; } = TypeId.Unnamed(typeof(IObjectRegistry));
        public static TypeId ObjectFactory { get; } = TypeId.Unnamed(typeof(IObjectFactory));
        public static TypeId ObjectCache { get; } = TypeId.Unnamed(typeof(IObjectCache));
        public static TypeId TypeCache { get; } = TypeId.Unnamed(typeof(ITypeCache));
    }
}
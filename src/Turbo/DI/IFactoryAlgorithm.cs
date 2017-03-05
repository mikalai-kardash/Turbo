namespace Turbo.DI
{
    public interface IFactoryAlgorithm
    {
        object GetInstance(TypeId id);
        object CreateInstance(TypeId id);
    }
}
namespace Turbo
{
    public interface IObjectFactory
    {
        T GetInstance<T>();
    }
}
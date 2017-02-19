namespace Turbo.DI
{
    internal interface IObjectRegistry
    {
        void RegisterInstance<T>(T instance);
        Registration<TFrom, TTo> RegisterType<TFrom, TTo>() where TTo : TFrom;
    }
}
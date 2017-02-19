namespace Turbo.DI
{
    public class Registration<TFrom, TTo> : Registration
        where TTo : TFrom
    {
        public Registration()
            : base(typeof(TFrom), typeof(TTo))
        {
        }

        public Registration<TFrom, TTo> DependsOn<T>()
        {
            AddDependency(typeof(T));
            return this;
        }
    }
}
namespace Turbo.DI
{
    public static class RegistrationExtensions
    {
        public static Registration DependsOn<T>(this Registration registration)
        {
            registration.AddDependency(typeof(T));
            return registration;
        }

        public static Registration Cached(this Registration registration)
        {
            registration.ShouldCache = true;
            return registration;
        }
    }
}
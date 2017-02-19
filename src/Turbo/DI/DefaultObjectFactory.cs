using System;
using System.Collections.Generic;
using System.Linq;

namespace Turbo.DI
{
    internal class DefaultObjectFactory : IObjectFactory, IObjectRegistry
    {
        private readonly IDictionary<Type, object> _instances
            = new Dictionary<Type, object>();

        private readonly IDictionary<Type, Registration> _registrations
            = new Dictionary<Type, Registration>();

        public DefaultObjectFactory()
        {
            RegisterInstance(this);
            RegisterInstance<IObjectFactory>(this);
            RegisterInstance<IObjectRegistry>(this);
        }

        public void RegisterInstance<T>(T instance)
        {
            _instances.Add(typeof(T), instance);
        }

        public T GetInstance<T>()
        {
            return (T) GetInstance(typeof(T));
        }

        public object GetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                object instance;
                if (_instances.TryGetValue(type, out instance))
                {
                    return instance;
                }

                Registration r;
                if (!_registrations.TryGetValue(type, out r))
                {
                    // When type is not registered - try to create it anyways.
                    return Activator.CreateInstance(type);
                }

                var ds = r.Dependencies.Select(GetInstance).ToArray();
                return Activator.CreateInstance(r.To, ds);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to create {type}.", ex);
            }
        }

        public Registration<TFrom, TTo> RegisterType<TFrom, TTo>()
            where TTo : TFrom
        {
            var registration = new Registration<TFrom, TTo>();
            _registrations.Add(registration.From, registration);
            return registration;
        }
    }
}
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

                var r = FindFor(type);
                if (r == null)
                {
                    // When type is not registered - try to create it anyways.
                    return Activator.CreateInstance(type);
                }

                var ds = r.Dependencies.Select(GetInstance).ToArray();
                return Activator.CreateInstance(r.GetConstructionType(type), ds);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to create {type}.", ex);
            }
        }

        private Registration FindFor(Type type)
        {
            Registration r;
            if (_registrations.TryGetValue(type, out r))
            {
                return r;
            }

            foreach (var registration in _registrations.Values)
            {
                if (registration.IsRelatedTo(type))
                {
                    r = registration;
                    break;
                }
            }

            return r;
        }

        public Registration<TFrom, TTo> RegisterType<TFrom, TTo>()
            where TTo : TFrom
        {
            var registration = new Registration<TFrom, TTo>();
            _registrations.Add(registration.From, registration);
            return registration;
        }

        public Registration RegisterType(Type from, Type to)
        {
            var registration = new Registration(from, to);
            _registrations.Add(registration.From, registration);
            return registration;
        }
    }
}
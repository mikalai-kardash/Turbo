using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Turbo.DI
{
    internal class DefaultObjectFactory : IObjectFactory, IObjectRegistry
    {
        private readonly IDictionary<Type, object> _instances
            = new Dictionary<Type, object>();

        private readonly IDictionary<Type, Registration> _registrations
            = new Dictionary<Type, Registration>();

        private readonly IDictionary<Type, Delegate> _expressions
            = new Dictionary<Type, Delegate>();

        private bool _disposed;

        public DefaultObjectFactory()
        {
            var registry = this as IObjectRegistry;

            registry.Instance(this);
            registry.Instance<IObjectFactory>(this);
            registry.Instance<IObjectRegistry>(this);
        }

        #region Factory

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
                    var l = Expression.Lambda(Expression.New(type));
                    var c = l.Compile();

                    return c.DynamicInvoke();
                }

                var ds = r.Dependencies.Select(GetInstance).ToArray();
                var ct = r.GetConstructionType(type);

                return Activator.CreateInstance(ct, ds);
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

        #endregion

        #region Registry

        Registration[] IObjectRegistry.Registrations => _registrations.Values.ToArray();

        Registration IObjectRegistry.RegisterType(Type from, Type to)
        {
            var registration = new Registration(from, to);
            _registrations.Add(registration.From, registration);
            return registration;
        }

        void IObjectRegistry.RegisterInstance(Type type, object instance)
        {
            _instances.Add(type, instance);
        }

        protected void Uses<T>() where T: Module, new()
        {
            var module = new T() as IObjectRegistry;
            var all = module.Registrations;

            foreach (var registration in all)
            {
                _registrations.Add(registration.From, registration);
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (!disposing)
            {
                return;
            }

            _instances.Clear();

            _disposed = true;
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Turbo.DI
{
    internal class DefaultObjectFactory : IObjectFactory, IObjectRegistry
    {
        private readonly IDictionary<TypeId, object> _instances
            = new Dictionary<TypeId, object>();

        private readonly IDictionary<TypeId, Registration> _registrations
            = new Dictionary<TypeId, Registration>();

        private readonly IDictionary<TypeId, Delegate> _expressions
            = new Dictionary<TypeId, Delegate>();

        private bool _disposed;

        public DefaultObjectFactory()
        {
            var registry = this as IObjectRegistry;

            registry.Instance(this);
            registry.Instance<IObjectFactory>(this);
            registry.Instance<IObjectRegistry>(this);
        }

        #region Factory

        public object GetInstance(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var typeId = new TypeId(type, name);

            try
            {
                object instance;
                if (_instances.TryGetValue(typeId, out instance))
                {
                    return instance;
                }

                object o;

                var r = FindFor(typeId);
                if (r == null)
                {
                    o = CreateUnknownType(type);
                }
                else
                {
                    // todo: name for dependency
                    var ds = r.Dependencies
                        .Select(t => GetInstance(t, string.Empty))
                        .ToArray();

                    var ct = r.GetConstructionType(type);

                    o = Activator.CreateInstance(ct, ds);
                }

                return o;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to create {type}.", ex);
            }
        }

        private static object CreateUnknownType(Type type)
        {
            var l = Expression.Lambda(Expression.New(type));
            var c = l.Compile();
            return c.DynamicInvoke();
        }

        private Registration FindFor(TypeId typeId)
        {
            Registration r;
            if (_registrations.TryGetValue(typeId, out r))
            {
                return r;
            }

            foreach (var registration in _registrations.Values)
            {
                if (registration.IsRelatedTo(typeId))
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

        Registration IObjectRegistry.RegisterType(Type from, Type to, string name)
        {
            var registration = new Registration(from, to, name);
            _registrations.Add(registration.Id, registration);
            return registration;
        }

        void IObjectRegistry.RegisterInstance(Type type, object instance, string name)
        {
            var typeId = new TypeId(type, name);
            _instances.Add(typeId, instance);
        }

        protected void Uses<T>() where T: Module, new()
        {
            var module = new T() as IObjectRegistry;
            var all = module.Registrations;

            foreach (var registration in all)
            {
                _registrations.Add(registration.Id, registration);
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
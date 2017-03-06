using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Turbo.DI
{
    internal class DefaultObjectFactory : ObjectFactoryTemplate, IObjectRegistry
    {
        private readonly IObjectCache _objectCache = new ObjectCache();

        private readonly IDictionary<TypeId, Registration> _registrations
            = new Dictionary<TypeId, Registration>();

        private bool _disposed;

        public DefaultObjectFactory()
        {
            var registry = this as IObjectRegistry;

            registry.Instance(this);
            registry.InstanceOfObjectRegistry();
            registry.InstanceOfObjectFactory(this);
            registry.Instance(_objectCache);
        }

        #region Factory

        #endregion

        #region Algorithm

        public override object GetInstance(TypeId id)
        {
            return _objectCache.Get(id);
        }

        public override object CreateInstance(TypeId id)
        {
            var r = FindTypeRegistration(id);
            if (r == null) return null;

            var o = CreateObject(id, r);

            if (r.ShouldCache)
            {
                CacheObject(id, o);
            }

            return o;
        }

        private Registration FindTypeRegistration(TypeId typeId)
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

        private object CreateObject(TypeId id, Registration r)
        {
            var ds = r.Dependencies
                .Select(t => GetInstance(t, string.Empty))
                .ToArray();

            var ct = r.GetConstructionType(id.Type);

            return Activator.CreateInstance(ct, ds);
        }

        private void CacheObject(TypeId typeId, object instance)
        {
            _objectCache.Add(typeId, instance);
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
            CacheObject(TypeId.Create(type, name), instance);
        }

        #endregion

        #region Dispose

        public override void Dispose()
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

            _objectCache.Dispose();
            _disposed = true;
        }

        #endregion

        protected void Include<T>() where T : Module, new()
        {
            var module = new T();

            var registry = module.GetInstance<IObjectRegistry>();

            var registrations = registry.Registrations;
            foreach (var registration in registrations)
            {
                _registrations.Add(registration.Id, registration);
            }

            var factory = module.GetInstance<IObjectFactory>();
            var cache = factory.GetInstance<IObjectCache>();

            foreach (var id in cache.AllObjectIds)
            {
                if (id == InternalTypeIds.DefaultObjectFactory) continue;
                if (id == InternalTypeIds.ObjectRegistry) continue;
                if (id == InternalTypeIds.ObjectFactory) continue;
                if (id == InternalTypeIds.ObjectCache) continue;

                var i = factory.GetInstance(id.Type, id.Name);
                _objectCache.Add(id, i);
            }
        }

        public static object CreateUnknownType(Type type)
        {
            if (type.IsInterface || type.IsAbstract)
            {
                throw new InvalidOperationException(
                    $"Cannot create instance of interface or abstract class: {type.Name} ({type}).");
            }
            var c = CreateUnknownTypeExpression(type).Compile();
            return c.DynamicInvoke();
        }

        public static LambdaExpression CreateUnknownTypeExpression(Type type)
        {
            return Expression.Lambda(Expression.New(type));
        }
    }
}
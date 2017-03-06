using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Turbo.DI
{
    internal class DefaultObjectFactory : ObjectFactoryTemplate, IObjectRegistry
    {
        private readonly IObjectCache _objectCache = new ObjectCache();
        private readonly ITypeCache _typeCache = new TypeCache();

        private bool _disposed;

        public DefaultObjectFactory()
        {
            var registry = this as IObjectRegistry;

            registry.Instance(this);
            registry.InstanceOfObjectRegistry();
            registry.InstanceOfObjectFactory(this);
            registry.Instance(_objectCache);
            registry.Instance(_typeCache);
        }

        #region Algorithm

        public override object GetInstance(TypeId id)
        {
            return _objectCache.Get(id);
        }

        public override object CreateInstance(TypeId id)
        {
            var registration = _typeCache.Find(id);
            if (registration == null) return null;

            var o = CreateObject(
                registration.GetConstructionType(id.Type), 
                registration.Dependencies);

            if (registration.ShouldCache)
            {
                CacheObject(id, o);
            }

            return o;
        }

        private object CreateObject(Type type, IEnumerable<Type> dependencies)
        {
            var ds = dependencies
                .Select(t => GetInstance(t, string.Empty))
                .ToArray();

            return Activator.CreateInstance(type, ds);
        }

        private void CacheObject(TypeId typeId, object instance)
        {
            _objectCache.Add(typeId, instance);
        }

        #endregion

        #region Registry

        Registration IObjectRegistry.RegisterType(Type from, Type to, string name)
        {
            var registration = new Registration(from, to, name);
            _typeCache.Add(registration);
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

            var typeCache = module.GetInstance<ITypeCache>();
            foreach (var registration in typeCache.All)
            {
                _typeCache.Add(registration);
            }

            var cache = module.GetInstance<IObjectCache>();
            foreach (var id in cache.AllObjectIds)
            {
                if (id == InternalTypeIds.DefaultObjectFactory) continue;
                if (id == InternalTypeIds.ObjectRegistry) continue;
                if (id == InternalTypeIds.ObjectFactory) continue;
                if (id == InternalTypeIds.ObjectCache) continue;
                if (id == InternalTypeIds.TypeCache) continue;

                var i = module.GetInstance(id.Type, id.Name);
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
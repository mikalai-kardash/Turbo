using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Turbo.DI
{
    public class SimpleObjectFactory : ObjectFactoryTemplate, IObjectFactoryRegistry
    {
        private readonly IObjectCache _objectCache = new ObjectCache();
        private readonly ITypeCache _typeCache = new TypeCache();

        private bool _disposed;

        public SimpleObjectFactory()
        {
            var registry = this as IObjectFactoryRegistry;

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
                _objectCache.Add(id, o);
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

        #endregion

        #region Registry

        Registration IObjectFactoryRegistry.RegisterType(Type from, Type to, string name)
        {
            var registration = new Registration(from, to, name);
            _typeCache.Add(registration);
            return registration;
        }

        void IObjectFactoryRegistry.RegisterInstance(Type type, object instance, string name)
        {
            var id = TypeId.Create(type, name);
            _objectCache.Add(id, instance);
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
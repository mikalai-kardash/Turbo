using System;

namespace Turbo.DI
{
    public abstract class ObjectFactoryTemplate : IFactoryAlgorithm, IObjectFactory
    {
        public object GetInstance(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var typeId = TypeId.Create(type, name);

            try
            {
                var instance = GetInstance(typeId);
                if (instance != null)
                {
                    return instance;
                }

                instance = CreateInstance(typeId);
                if (instance != null)
                {
                    return instance;
                }

                return SimpleObjectFactory.CreateUnknownType(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to create {type}.", ex);
            }
        }

        public virtual void Dispose()
        {
        }

        public abstract object GetInstance(TypeId id);
        public abstract object CreateInstance(TypeId id);
    }
}
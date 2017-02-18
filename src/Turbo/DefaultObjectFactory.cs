using System;

namespace Turbo
{
    internal class DefaultObjectFactory : IObjectFactory
    {
        public T GetInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public object GetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Activator.CreateInstance(type);
        }
    }
}
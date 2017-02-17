using System;

namespace Turbo
{
    internal class DefaultObjectFactory : IObjectFactory
    {
        public T GetInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Turbo.DI
{
    public class Registration
    {
        private readonly IList<Type> _dependencies = new List<Type>();

        public Registration(Type from, Type to, string name)
        {
            Id = TypeId.Create(from, name);
            To = to;

            Dependencies = _dependencies;
        }

        public TypeId Id { get; }
        public Type To { get; }

        public IEnumerable<Type> Dependencies { get; }
        public bool ShouldCache { get; internal set; }

        public void AddDependency(Type type)
        {
            _dependencies.Add(type);
        }

        public bool IsRelatedTo(TypeId other)
        {
            var type = other.Type;
            if (!Id.Type.IsConstructedGenericType && type.IsGenericType)
            {
                return Id.Type == type.GetGenericTypeDefinition();
            }

            return type == Id.Type;
        }

        public Type GetConstructionType(Type type)
        {
            if (type.IsGenericType && !Id.Type.IsConstructedGenericType)
            {
                var args = type.GetGenericArguments();
                return To.MakeGenericType(args);
            }

            return To;
        }
    }
}
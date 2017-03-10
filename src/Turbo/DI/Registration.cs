using System;
using System.Collections.Generic;
using System.Reflection;

namespace Turbo.DI
{
    public class Registration
    {
        private readonly HashSet<Type> _dependencies = new HashSet<Type>();

        public Registration(Type from, Type to, string name)
        {
            Id = TypeId.Create(from, name);
            To = to;

            InitFromConstructor(to);
        }

        private void InitFromConstructor(Type to)
        {
            // makes no sense to register anything if no public ctors
            var ctors = to.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (ctors.Length != 1) return;

            var parameters = ctors[0].GetParameters();
            foreach (var p in parameters)
            {
                AddDependency(p.ParameterType);
            }
        }

        public TypeId Id { get; }
        public Type To { get; }

        public IEnumerable<Type> Dependencies => _dependencies;

        public bool ShouldCache { get; internal set; }

        public void AddDependency(Type type)
        {
            if (!_dependencies.Contains(type))
            {
                _dependencies.Add(type);
            }
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

        public void AddAlias(Type type)
        {
        }
    }
}
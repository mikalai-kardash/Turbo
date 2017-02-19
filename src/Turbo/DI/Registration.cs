using System;
using System.Collections.Generic;

namespace Turbo.DI
{
    public class Registration
    {
        private readonly IList<Type> _dependencies = new List<Type>();

        public Type From { get; }
        public Type To { get; }

        public Registration(Type from, Type to)
        {
            From = from;
            To = to;

            Dependencies = _dependencies;
        }

        protected void AddDependency(Type type)
        {
            _dependencies.Add(type);
        }

        public IEnumerable<Type> Dependencies { get; }
    }
}
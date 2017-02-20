using System;
using System.Reflection;

namespace Turbo.Construction.Target
{
    public class PropertyTarget : ITarget
    {
        private readonly PropertyInfo _property;

        public PropertyTarget(PropertyInfo property)
        {
            _property = property;
        }

        public string Name => _property.Name;
        public Type TargetType => _property.PropertyType;

        public void SetValue(object instance, object value)
        {
            _property.SetValue(instance, value);
        }

        public Type GetTypeOfArray()
        {
            return _property.PropertyType.GetElementType();
        }

        public Type GetTargetClass()
        {
            return _property.DeclaringType;
        }
    }
}
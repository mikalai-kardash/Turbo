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

        public bool IsPublic
        {
            get
            {
                if (!_property.CanWrite) return false;
                var setter = _property.GetSetMethod(true);
                return setter.IsPublic;
            }
        }

        public bool IsArray => _property.PropertyType.IsArray;

        public bool IsClass => _property.PropertyType.IsClass;

        public Type TargetType => _property.PropertyType;

        public Type GetTypeOfArray()
        {
            return _property.PropertyType.GetElementType();
        }

        public Type GetTargetClass()
        {
            return _property.DeclaringType;
        }

        public void SetValue(object instance, object value)
        {
            _property.SetValue(instance, value);
        }
    }
}
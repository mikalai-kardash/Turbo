﻿using System;
using System.Reflection;

namespace Turbo.Construction.Target
{
    public class FieldTarget : ITarget
    {
        private readonly FieldInfo _field;

        public FieldTarget(FieldInfo field)
        {
            _field = field;
        }

        public string Name => _field.Name;
        public bool IsPublic => _field.IsPublic;
        public Type TargetType => _field.FieldType;
        public bool IsArray => _field.FieldType.IsArray;
        public bool IsClass => _field.FieldType.IsClass;

        public void SetValue(object instance, object value)
        {
            _field.SetValue(instance, value);
        }

        public Type GetTypeOfArray()
        {
            return _field.FieldType.GetElementType();
        }

        public Type GetTargetClass()
        {
            return _field.DeclaringType;
        }
    }
}
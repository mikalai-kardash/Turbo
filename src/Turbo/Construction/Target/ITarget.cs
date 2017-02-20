using System;

namespace Turbo.Construction.Target
{
    public interface ITarget
    {
        string Name { get; }
        Type TargetType { get; }
        bool IsArray { get; }
        bool IsClass { get; }

        void SetValue(object instance, object value);
        Type GetTypeOfArray();
        Type GetTargetClass();
    }
}
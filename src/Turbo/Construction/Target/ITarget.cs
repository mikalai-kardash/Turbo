using System;

namespace Turbo.Construction.Target
{
    public interface ITarget
    {
        string Name { get; }

        bool IsArray { get; }
        bool IsClass { get; }
        bool IsPublic { get; }

        Type TargetType { get; }
        Type GetTypeOfArray();
        Type GetTargetClass();

        void SetValue(object instance, object value);
    }
}
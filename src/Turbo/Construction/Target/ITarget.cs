using System;

namespace Turbo.Construction.Target
{
    public interface ITarget
    {
        string Name { get; }
        Type TargetType { get; }

        void SetValue(object instance, object value);

        Type GetTypeOfArray();
        Type GetTargetClass();
    }
}
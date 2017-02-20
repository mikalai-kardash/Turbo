using System.Reflection;

namespace Turbo.Construction.Target
{
    public static class Extensions
    {
        public static ITarget ToTarget(this FieldInfo field)
        {
            return new FieldTarget(field);
        }

        public static ITarget ToTarget(this PropertyInfo property)
        {
            return new PropertyTarget(property);
        }
    }
}
using System.Collections.Generic;

namespace Turbo.Events
{
    public static class ListExtensions
    {
        public static void AddIf<T>(this IList<T> list, object instance)
            where T : class
        {
            var i = instance as T;
            if (i != null)
            {
                list.Add(i);
            }
        }
    }
}
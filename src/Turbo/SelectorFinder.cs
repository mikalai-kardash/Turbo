using System;
using System.Collections.Generic;
using System.Linq;

namespace Turbo
{
    public interface ISelectorFinder
    {
        string GetSelector(string fieldName);
    }

    public class NullSelectorFinder : ISelectorFinder
    {
        public string GetSelector(string fieldName)
        {
            return string.Empty;
        }
    }

    public class SelectorFinder : ISelectorFinder
    {
        private readonly IDictionary<string, string> _elements;

        public SelectorFinder(IDictionary<string, string> elements)
        {
            _elements = elements.ToDictionary(
                kvp => GetKey(kvp.Key),
                kvp => kvp.Value,
                StringComparer.OrdinalIgnoreCase);
        }

        private static string GetKey(string key)
        {
            return key
                .Replace(" ", string.Empty)
                .Replace("_", string.Empty)
                .Replace("-", string.Empty)
                .Trim();
        }

        private static string GetField(string field)
        {
            return field.Replace("_", string.Empty);
        }

        public string GetSelector(string fieldName)
        {
            string key;
            var field = GetField(fieldName);
            _elements.TryGetValue(field, out key);
            return key;
        }
    }
}
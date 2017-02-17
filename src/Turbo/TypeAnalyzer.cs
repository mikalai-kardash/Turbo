using System;
using System.Reflection;
using OpenQA.Selenium;

namespace Turbo
{
    public class TypeAnalyzer
    {
        private const BindingFlags All = BindingFlags.Instance |
                                         BindingFlags.NonPublic |
                                         BindingFlags.Public;

        public static void Analyze<T>(PageBuilder builder)
        {
            Analyze(typeof(T), builder);
        }

        public static void Analyze(Type t, PageBuilder builder)
        {
            Fields(builder, t);
            Properties(builder, t);
        }

        private static void Properties(PageBuilder builder, Type type)
        {
            var properties = type.GetProperties(All);

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(IWebElement))
                {
                    builder.SetWebElement(property);
                    continue;
                }

                if (property.PropertyType == typeof(IWebDriver))
                {
                    builder.SetWebDriver(property);
                    continue;
                }

                if (property.PropertyType.IsClass)
                {
                    builder.SetPagePart(property);
                    continue;
                }
            }
        }

        private static void Fields(PageBuilder builder, Type type)
        {
            var fields = type.GetFields(All);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(IWebDriver))
                {
                    builder.SetWebDriver(field);
                    continue;
                }

                if (field.FieldType == typeof(IWebElement))
                {
                    builder.SetWebElement(field);
                    continue;
                }

                if (field.FieldType.IsClass)
                {
                    builder.SetPagePart(field);
                    continue;
                }
            }
        }
    }
}
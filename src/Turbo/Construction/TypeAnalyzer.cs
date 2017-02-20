using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using Turbo.Construction.Target;

namespace Turbo.Construction
{
    public static class TypeAnalyzer
    {
        private const BindingFlags All = BindingFlags.Instance |
                                         BindingFlags.NonPublic |
                                         BindingFlags.Public;

        public static void Analyze<T>(IPageBuilder builder)
        {
            Analyze(typeof(T), builder);
        }

        public static void Analyze(Type t, IPageBuilder builder)
        {
            Fields(builder, t);
            Properties(builder, t);
        }

        private static void Properties(IPageBuilder builder, Type type)
        {
            var properties = type.GetProperties(All);

            foreach (var property in properties)
            {
                if (!property.CanWrite)
                {
                    continue;
                }
                
                if (property.PropertyType == typeof(IWebElement))
                {
                    builder.SetWebElement(property.ToTarget());
                    continue;
                }

                if (property.PropertyType.IsClass)
                {
                    if (property.PropertyType.IsArray)
                    {
                        if (property.PropertyType.GetElementType() == typeof(IWebElement))
                        {
                            builder.SetWebElementCollection(property.ToTarget());
                            continue;
                        }

                        builder.SetPartCollection(property.ToTarget());
                        continue;
                    }

                    builder.SetPart(property.ToTarget());
                    continue;
                }
            }
        }

        private static void Fields(IPageBuilder builder, Type type)
        {
            var fields = type.GetFields(All);

            foreach (var field in fields)
            {
                if (field.IsInitOnly)
                {
                    // Do not set read-only fields because its 
                    // behavior depends on the environment.
                    continue;
                }

                if (field.FieldType == typeof(IWebDriver))
                {
                    builder.SetWebDriver(field.ToTarget());
                    continue;
                }

                if (field.FieldType == typeof(IWebElement))
                {
                    // Compiler Generated baking fields receive "special"
                    // names and cannot be so easily used to find selectors later.
                    if (!field.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    {
                        builder.SetWebElement(field.ToTarget());
                    }
                    continue;
                }

                if (field.FieldType.IsClass)
                {
                    if (field.FieldType.IsArray)
                    {
                        if (field.FieldType.GetElementType() == typeof(IWebElement))
                        {
                            builder.SetWebElementCollection(field.ToTarget());
                            continue;
                        }

                        builder.SetPartCollection(field.ToTarget());
                        continue;
                    }

                    builder.SetPart(field.ToTarget());
                    continue;
                }
            }
        }
    }
}
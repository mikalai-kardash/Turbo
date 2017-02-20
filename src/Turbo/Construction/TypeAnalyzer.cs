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

                if (property.IsDefined(typeof(CompilerGeneratedAttribute), false))
                {
                    continue;
                }

                AnalyzeTarget(builder, property.ToTarget());
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

                if (field.IsDefined(typeof(CompilerGeneratedAttribute), false))
                {
                    continue;
                }

                AnalyzeTarget(builder, field.ToTarget());
            }
        }

        private static void AnalyzeTarget(IPageBuilder builder, ITarget target)
        {
            if (target.TargetType == typeof(IWebDriver))
            {
                builder.SetWebDriver(target);
                return;
            }

            if (target.TargetType == typeof(IWebElement))
            {
                builder.SetWebElement(target);
                return;
            }

            if (!target.IsClass)
            {
                return;
            }

            if (target.IsArray)
            {
                if (target.GetTypeOfArray() == typeof(IWebElement))
                {
                    builder.SetWebElementCollection(target);
                    return;
                }

                builder.SetPartCollection(target);
                return;
            }

            builder.SetPart(target);
        }
    }
}
using System;
using OpenQA.Selenium;

namespace Turbo.Construction.Steps.AssignPart
{
    public interface IAssignPart
    {
        void Run(IWebDriver driver, object instance, Func<Type, object> createInstance);
        void Run(IWebDriver driver, IWebElement parent, object instance, Func<Type, object> createInstance);
    }
}
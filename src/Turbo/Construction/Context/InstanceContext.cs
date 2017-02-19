using System;
using OpenQA.Selenium;

namespace Turbo.Construction.Context
{
    public class InstanceContext
    {
        public object Instance { get; set; }
        public Func<Type, object> Factory { get; set; }
        public IWebDriver Driver { get; set; }
        public IWebElement Root { get; set; }

        public ExecutionContext ToExecution()
        {
            return new ExecutionContext
            {
                Driver = Driver,
                Parent = Root,
                Factory = Factory
            };
        }
    }
}
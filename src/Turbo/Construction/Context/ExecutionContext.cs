using System;
using OpenQA.Selenium;

namespace Turbo.Construction.Context
{
    public class ExecutionContext
    {
        public Func<Type, object> Factory { get; set; }
        public IWebDriver Driver { get; set; }
        public IWebElement Parent { get; set; }

        public InstanceContext ToInstance(Type type)
        {
            return new InstanceContext
            {
                Driver = Driver,
                Factory = Factory,
                Instance = Factory(type)
            };
        }

        public InstanceContext ToInstance(Type type, IWebElement root)
        {
            return new InstanceContext
            {
                Driver = Driver,
                Root = root,
                Factory = Factory,
                Instance = Factory(type)
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;

namespace Turbo
{
    public class Analysis
    {
        private readonly List<Action<object, IWebDriver>> _schedule 
            = new List<Action<object, IWebDriver>>();

        public bool IsDone => _schedule.Count > 0;

        public object Activate(object instance, IWebDriver driver)
        {
            foreach (var action in _schedule)
            {
                action(instance, driver);
            }

            return instance;
        }

        public void AssignWithWebDriver(FieldInfo field)
        {
            var f = field;
            _schedule.Add((o, d) => { f.SetValue(o, d); });
        }

        public void Assign(FieldInfo field, string selector)
        {
            var f = field;
            var l = selector;

            _schedule.Add((o, d) =>
            {
                var by = By.CssSelector(l);
                var element = d.FindElement(by);

                f.SetValue(o, element);
            });
        }
    }
}
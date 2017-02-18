using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using Turbo.Construction.Steps.AssignPart;
using Turbo.Construction.Steps.FindElement;
using Turbo.Construction.Steps.Root;
using Turbo.Construction.Steps.WebDriver;

namespace Turbo.Construction
{
    public class Analysis
    {
        private readonly List<ISetWebDriver> _assignWebDriver 
            = new List<ISetWebDriver>();

        private readonly List<FindElement> _findElement 
            = new List<FindElement>();

        private readonly List<IAssignRootElement> _assignRoot 
            = new List<IAssignRootElement>();

        private readonly List<IAssignPart> _assignPart 
            = new List<IAssignPart>();

        private IWebElement _root;

        public bool IsDone => _assignWebDriver.Count > 0
                              || _findElement.Count > 0
                              || _assignRoot.Count > 0
                              || _assignPart.Count > 0;

        public string RootCssSelector { get; set; }
        public Type Type { get; set; }

        public void AssignWithWebDriver(FieldInfo field)
        {
            _assignWebDriver.Add(new AssignWebDriver(field));
        }

        public void Assign(FieldInfo field, string selector)
        {
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentNullException(nameof(selector));
            }

            _findElement.Add(new FindElement(field, selector));
        }

        public void Assign(PropertyInfo property, string selector)
        {
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentNullException(nameof(selector));
            }

            _findElement.Add(new FindElement(property, selector));
        }

        public void AssignRoot(FieldInfo field)
        {
            _assignRoot.Add(new AssignRootElement(field));
        }

        public void AssignRoot(PropertyInfo property)
        {
            _assignRoot.Add(new AssignRootElement(property));
        }

        public void AssignPart(FieldInfo field, PartInfo partInfo)
        {
            _assignPart.Add(new AssignPart(field, partInfo));
        }

        public void AssignPartCollection(FieldInfo field, PartInfo partInfo)
        {
            _assignPart.Add(new AssignPartCollection(field, partInfo));
        }

        public void AssignRootElement(string selector, Analysis parent)
        {
        }

        public object Activate(IWebDriver driver, IWebElement parent, Func<Type, object> createInstance)
        {
            return ActivateCollection(driver, parent, createInstance).FirstOrDefault();
        }

        public object Activate(IWebDriver driver, Func<Type, object> createInstance)
        {
            return ActivateCollection(driver, null, createInstance).FirstOrDefault();
        }

        public IEnumerable<object> ActivateCollection(IWebDriver driver, IWebElement parent, Func<Type, object> createInstance)
        {
            if (string.IsNullOrWhiteSpace(RootCssSelector))
            {
                var instance = createInstance(Type);

                AssignWebDriversForObject(driver, instance);
                FindAllElements(driver, instance);
                AssignParts(driver, createInstance, instance);

                yield return instance;
            }
            else
            {
                var rootElements = GetAllRootElements(driver, parent);

                foreach (var root in rootElements)
                {
                    var instance = createInstance(Type);

                    AssignWebDriversForObject(driver, instance);
                    AssignRootElementForObject(root, instance);
                    FindAllElements(root, instance);

                    foreach (var assignPart in _assignPart)
                    {
                        assignPart.Run(driver, root, instance, createInstance);
                    }

                    yield return instance;
                }
            }
        }

        private void AssignParts(IWebDriver driver, Func<Type, object> createInstance, object instance)
        {
            foreach (var assignPart in _assignPart)
            {
                assignPart.Run(driver, instance, createInstance);
            }
        }

        private void FindAllElements(IWebElement root, object instance)
        {
            foreach (var findElement in _findElement)
            {
                findElement.Run(root, instance);
            }
        }

        private void AssignRootElementForObject(IWebElement rootElement, object instance)
        {
            foreach (var assignRootElement in _assignRoot)
            {
                assignRootElement.Run(rootElement, instance);
            }
        }

        private IEnumerable<IWebElement> GetAllRootElements(IWebDriver driver, IWebElement parent)
        {
            var by = By.CssSelector(RootCssSelector);

            return parent != null 
                ? parent.FindElements(by) 
                : driver.FindElements(by);
        }

        private void FindAllElements(IWebDriver driver, object instance)
        {
            foreach (var findElement in _findElement)
            {
                findElement.Run(driver, instance);
            }
        }

        private void AssignWebDriversForObject(IWebDriver driver, object instance)
        {
            foreach (var step in _assignWebDriver)
            {
                step.Run(driver, instance);
            }
        }
    }
}
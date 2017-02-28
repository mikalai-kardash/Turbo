using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Turbo.Cache.Info;
using Turbo.Construction.Context;
using Turbo.Construction.Steps.AssignPart;
using Turbo.Construction.Steps.FindElement;
using Turbo.Construction.Steps.Root;
using Turbo.Construction.Steps.WaitForElement;
using Turbo.Construction.Steps.WebDriver;
using Turbo.Construction.Target;

namespace Turbo.Construction
{
    public class Analysis
    {
        private readonly List<ISetWebDriver> _setWebDriver
            = new List<ISetWebDriver>();

        private readonly List<IFindElement> _findElement
            = new List<IFindElement>();

        private readonly List<IAssignRootElement> _assignRoot
            = new List<IAssignRootElement>();

        private readonly List<IAssignPart> _assignPart
            = new List<IAssignPart>();

        private readonly List<IWaitForElement> _wait 
            = new List<IWaitForElement>();

        public bool IsDone { get; set; }

        public string RootSelector { get; set; }

        public Type Type { get; set; }

        public void AssignWithWebDriver(ITarget target)
        {
            _setWebDriver.Add(new SetWebDriver(target));
        }

        public void Assign(ITarget target, string selector)
        {
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentNullException(nameof(selector), 
                    $"Selector is not provided for field '{target.Name}' declared on '{target.GetTargetClass()}' type.");
            }

            _findElement.Add(new FindElement(target, selector));
        }

        public void AssignCollection(ITarget target, string selector)
        {
            _findElement.Add(
                new FindElementCollection(target, selector));
        }

        public void AssignRoot(ITarget target)
        {
            _assignRoot.Add(new AssignRootElement(target));
        }

        public void AssignPart(ITarget target, PartInfo partInfo)
        {
            _assignPart.Add(new AssignPart(target, partInfo));
        }

        public void AssignPartCollection(ITarget target, PartInfo partInfo)
        {
            _assignPart.Add(
                new AssignPartCollection(target, partInfo));
        }

        public void WaitForElement(string waitSelector)
        {
            _wait.Add(new WaitForElement(waitSelector));
        }

        public object Activate(ExecutionContext context)
        {
            return ActivateCollection(context).FirstOrDefault();
        }

        public IEnumerable<object> ActivateCollection(ExecutionContext executionContext)
        {
            if (string.IsNullOrWhiteSpace(RootSelector))
            {
                var instanceContext = executionContext.ToInstance(Type);
                yield return GetInstance(instanceContext);
                yield break;
            }

            // todo: figure out if to switch to ISearchContext
            var searchContext = (ISearchContext) executionContext.Parent ?? executionContext.Driver;
            var roots = GetAllRootElements(searchContext);

            foreach (var root in roots)
            {
                var instanceContext = executionContext.ToInstance(Type, root);
                yield return GetInstance(instanceContext);
            }
        }

        private object GetInstance(InstanceContext context)
        {
            Wait(context);

            AssignWebDriversForObject(context.Driver, context.Instance);

            if (context.Root == null)
            {
                FindAllElements(context.Driver, context.Instance);
            }
            else
            {
                AssignRootElementForObject(context.Root, context.Instance);
                FindAllElements(context.Root, context.Instance);
            }

            AssignParts(context);

            return context.Instance;
        }

        private void Wait(InstanceContext context)
        {
            foreach (var waitForElement in _wait)
            {
                if (context.Root == null)
                {
                    waitForElement.Run(context.Driver);
                }
                else
                {
                    waitForElement.Run(context.Driver, context.Root);
                }
            }
        }

        private void AssignParts(InstanceContext context)
        {
            foreach (var assignPart in _assignPart)
            {
                assignPart.Run(context);
            }
        }

        private void FindAllElements(IWebDriver driver, object instance)
        {
            foreach (var findElement in _findElement)
            {
                findElement.Run(driver, instance);
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

        private IEnumerable<IWebElement> GetAllRootElements(ISearchContext searchContext)
        {
            var by = By.CssSelector(RootSelector);
            return searchContext.FindElements(by);
        }

        private void AssignWebDriversForObject(IWebDriver driver, object instance)
        {
            foreach (var step in _setWebDriver)
            {
                step.Run(driver, instance);
            }
        }
    }
}
using System.Linq;
using OpenQA.Selenium;
using Turbo.Construction.Target;

namespace Turbo.Construction.Steps.FindElement
{
    public class FindElementCollection : IFindElement
    {
        private readonly ITarget _target;
        private readonly string _cssSelector;

        public FindElementCollection(ITarget target, string cssSelector)
        {
            _target = target;
            _cssSelector = cssSelector;
        }

        public void Run(IWebDriver driver, object instance)
        {
            FindAndSet(driver, instance);
        }

        public void Run(IWebElement element, object instance)
        {
            FindAndSet(element, instance);
        }

        private void FindAndSet(ISearchContext driver, object instance)
        {
            var by = By.CssSelector(_cssSelector);
            var el = driver.FindElements(by);

            _target.SetValue(instance, el.ToArray());
        }
    }
}
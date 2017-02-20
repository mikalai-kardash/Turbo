using OpenQA.Selenium;
using Turbo.Construction.Target;

namespace Turbo.Construction.Steps.FindElement
{
    public class FindElement : IFindElement
    {
        private readonly ITarget _target;
        private readonly string _cssSelector;

        public FindElement(ITarget toTarget, string cssSelector)
        {
            _target = toTarget;
            _cssSelector = cssSelector;
        }

        public void Run(IWebDriver driver, object instance)
        {
            var by = By.CssSelector(_cssSelector);
            var el = driver.FindElement(by);

            _target.SetValue(instance, el);
        }

        public void Run(IWebElement element, object instance)
        {
            var by = By.CssSelector(_cssSelector);
            var el = element.FindElement(by);

            _target.SetValue(instance, el);
        }
    }
}
using System.Reflection;
using OpenQA.Selenium;

namespace Turbo.Construction.Steps.FindElement
{
    public class FindElement : IFindElementWithWebDriver, IFindElementWithWebElement
    {
        private readonly FieldInfo _field;
        private readonly PropertyInfo _property;
        private readonly string _cssSelector;

        public FindElement(FieldInfo field, string cssSelector)
        {
            _field = field;
            _cssSelector = cssSelector;
        }

        public FindElement(PropertyInfo property, string cssSelector)
        {
            _property = property;
            _cssSelector = cssSelector;
        }

        public void Run(IWebDriver driver, object instance)
        {
            var by = By.CssSelector(_cssSelector);
            var el = driver.FindElement(by);

            SetFieldOrProp(instance, el);
        }

        public void Run(IWebElement element, object instance)
        {
            var by = By.CssSelector(_cssSelector);
            var el = element.FindElement(by);

            SetFieldOrProp(instance, el);
        }

        private void SetFieldOrProp(object instance, IWebElement el)
        {
            if (_field != null)
            {
                _field.SetValue(instance, el);
            }
            else
            {
                _property.SetValue(instance, el);
            }
        }
    }
}
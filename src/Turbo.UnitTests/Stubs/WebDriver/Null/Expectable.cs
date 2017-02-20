using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class Expectable : ISearchContext
    {
        private readonly Expectations _expectations = new Expectations();

        public void Expect(string cssSelector, params NullElement[] elements)
        {
            var by = By.CssSelector(cssSelector);

            foreach (var e in elements)
            {
                e.FoundBy = by;
                e.Parent = this;
            }

            _expectations.Add(by, elements);
        }

        public IWebElement FindElement(By by)
        {
            NullElement[] elements;
            if (!_expectations.TryGet(by, out elements))
            {
                return GetDefault();
            }

            return elements.FirstOrDefault();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            NullElement[] elements;
            if (!_expectations.TryGet(by, out elements))
            {
                return new List<IWebElement>().AsReadOnly();
            }

            return elements
                .OfType<IWebElement>()
                .ToList()
                .AsReadOnly();
        }

        private IWebElement GetDefault()
        {
            return new NullElement
            {
                FoundBy = By.CssSelector("DEFAULT"),
                Parent = this
            };
        }
    }
}
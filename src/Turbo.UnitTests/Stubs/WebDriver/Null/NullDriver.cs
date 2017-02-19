using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullDriver : IWebDriver
    {
        private ReadOnlyCollection<NullElement> _findElementsExpectation;

        public IWebElement FindElement(By by)
        {
            return new NullElement
            {
                FoundBy = by
            };
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            if (_findElementsExpectation == null)
            {
                return new List<IWebElement>().AsReadOnly();
            }

            foreach (var element in _findElementsExpectation)
            {
                element.FoundBy = by;
            }

            return _findElementsExpectation
                .Select(e => e as IWebElement)
                .ToList()
                .AsReadOnly();
        }

        public void Dispose()
        {
        }

        public void Close()
        {
        }

        public void Quit()
        {
        }

        public IOptions Manage()
        {
            return new NullOptions();
        }

        public INavigation Navigate()
        {
            return new NullNavigation();
        }

        public ITargetLocator SwitchTo()
        {
            return new NullTargetLocator();
        }

        public string Url { get; set; }

        public string Title { get; }

        public string PageSource { get; }

        public string CurrentWindowHandle { get; }

        public ReadOnlyCollection<string> WindowHandles { get; }

        public void ExpectFindElements(IEnumerable<NullElement> returns)
        {
            _findElementsExpectation = new List<NullElement>(returns).AsReadOnly();
        }
    }
}
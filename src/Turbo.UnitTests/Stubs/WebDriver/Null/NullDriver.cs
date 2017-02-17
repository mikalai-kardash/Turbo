using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullDriver : IWebDriver
    {
        public IWebElement FindElement(By by)
        {
            return new NullWebElement();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return new List<IWebElement>().AsReadOnly();
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
    }
}
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullDriver : Expectable, IWebDriver
    {
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
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Turbo.Events;

namespace Turbo.Statistics
{
    public class WebDriverWrapper : IWebDriver
    {
        private readonly IWebDriver _driver;
        private readonly EventSink<WebDriverWrapper> _sink;

        public WebDriverWrapper(IWebDriver driver, EventSink<WebDriverWrapper> sink)
        {
            _driver = driver;
            _sink = sink;
        }

        public IWebElement FindElement(By by)
        {
            _sink.Info(EventIds.SelectorUsage, "Find element by {by}.", by);
            using (_sink.Measure(EventIds.FindElement, "Find element by {by}.", by))
            {
                return _driver.FindElement(by);
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driver.FindElements(by);
        }

        public void Dispose()
        {
            _driver.Dispose();
        }

        public void Close()
        {
            _driver.Close();
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public string Url
        {
            get { return _driver.Url; }
            set { _driver.Url = value; }
        }

        public string Title => _driver.Title;

        public string PageSource => _driver.PageSource;

        public string CurrentWindowHandle => _driver.CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;
    }
}
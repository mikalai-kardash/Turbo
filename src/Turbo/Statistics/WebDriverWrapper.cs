using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Turbo.Logging;

namespace Turbo.Statistics
{
    public class WebDriverWrapper : IWebDriver
    {
        private readonly IWebDriver _driver;
        private readonly ILogger<WebDriverWrapper> _logger;

        public WebDriverWrapper(IWebDriver driver, ILogger<WebDriverWrapper> logger)
        {
            _driver = driver;
            _logger = logger;
        }

        public IWebElement FindElement(By by)
        {
            _logger.Trace(WebDriverEvents.SelectorUsage, "Looking for element using {by}.", by);

            using (LoggerMessage.DefineScope<By>("Looking for element using {by}.")(_logger, by))
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
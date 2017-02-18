using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Pages.Properties
{
    public class PropertiesPage
    {
        public IWebDriver Browser { get; set; }
        private IWebDriver PrivateBrowser { get; set; }
        public IWebDriver ReadOnlyBrowser { get; }


        private IWebElement element { get; set; }
        public IWebElement Element { get; set; }
        public IWebElement _Element { get; }

        public IWebDriver GetPrivateBrowser()
        {
            return PrivateBrowser;
        }

        public IWebElement GetElement()
        {
            return element;
        }
    }
}
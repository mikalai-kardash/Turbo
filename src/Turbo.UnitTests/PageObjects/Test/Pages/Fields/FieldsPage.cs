using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Pages.Fields
{
    public class FieldsPage
    {
        // inject driver instance
        private IWebDriver browser;
        public IWebDriver Browser;

        // locate and set element
        private IWebElement _element;

        public IWebDriver GetBrowser()
        {
            return browser;
        }

        public IWebElement Get_element()
        {
            return _element;
        }
    }
}
using Automation.PageObjects.Apartments.Pages.MobileApps;
using OpenQA.Selenium;

namespace Automation.PageObjects.CustomerPortal.Parts.Header.Parts.Menu.Parts.MenuPopup
{
    public class MenuPopup
    {
        private IWebDriver _browser;

        private IWebElement _root;

        // links
        private IWebElement _customerTools;

        private IWebElement _myListings;
        private IWebElement _reviews;
        private IWebElement _adAnalytics;
        private IWebElement _mobileApps;

        public MobileAppsPage GoToMobileAppsPage()
        {
            _mobileApps.Click();
            return null;
        }

        public MenuPopup Open()
        {
            _root.Click();
            return this;
        }

        public bool IsVisible => _root.Displayed;
    }
}
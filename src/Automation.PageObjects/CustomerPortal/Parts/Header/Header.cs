using Automation.PageObjects.Apartments.Pages.MobileApps;
using Automation.PageObjects.CustomerPortal.Parts.Header.Parts.Menu;
using OpenQA.Selenium;

namespace Automation.PageObjects.CustomerPortal.Parts.Header
{
    public class Header
    {
        private IWebElement _signIn;

        public Menu Menu { get; set; }

        public MobileAppsPage GoToMobileAppsPage()
        {
            var popup = Menu.Popup;

            if (!popup.IsVisible)
            {
                popup.Open();
            }

            return popup.GoToMobileAppsPage();
        }
    }
}
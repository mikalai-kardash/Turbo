using Automation.PageObjects.CustomerPortal.Parts.Header.Parts.Menu.Parts.MenuPopup;
using OpenQA.Selenium;

namespace Automation.PageObjects.CustomerPortal.Parts.Header.Parts.Menu
{
    public class Menu
    {
        private IWebElement _menuButton;

        public MenuPopup Popup { get; set; }
    }
}
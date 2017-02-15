using OpenQA.Selenium;

namespace Automation.PageObjects.Apartments.Parts.Headers
{
    public abstract class CommonHeader
    {
        private readonly IWebElement _signInButton;
        private readonly IWebElement _signUpButton;
        private readonly IWebElement _localeSwitch;
    }
}
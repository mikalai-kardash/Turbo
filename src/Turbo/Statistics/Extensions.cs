using OpenQA.Selenium;

namespace Turbo.Statistics
{
    public static class Extensions
    {
        public static IWebDriver Wrap(IWebDriver driver)
        {
            return new WebDriverWrapper(driver, null);
        }
    }
}
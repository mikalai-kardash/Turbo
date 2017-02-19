using OpenQA.Selenium;

namespace Turbo
{
    public interface ITurboFactory
    {
        TPage GetPage<TApp, TPage>(IWebDriver driver);
    }
}
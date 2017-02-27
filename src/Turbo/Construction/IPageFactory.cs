using OpenQA.Selenium;

namespace Turbo.Construction
{
    public interface IPageFactory
    {
        T BuildPage<T>(IWebDriver driver);
        T BuildPart<T>(IWebDriver driver);
        T BuildPart<T>(IWebDriver driver, IWebElement parent);

        void NavigateToPageFirst();
    }
}
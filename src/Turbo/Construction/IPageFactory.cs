using OpenQA.Selenium;

namespace Turbo.Construction
{
    public interface IPageFactory
    {
        TPage Build<TPage>(IWebDriver driver);
        void NavigateToPageFirst();
    }
}
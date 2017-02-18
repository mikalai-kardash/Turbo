using OpenQA.Selenium;

namespace Turbo.Construction.Steps.WebDriver
{
    public interface ISetWebDriver
    {
        void Run(IWebDriver driver, object instance);
    }
}
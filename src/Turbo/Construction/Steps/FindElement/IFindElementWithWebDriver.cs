using OpenQA.Selenium;

namespace Turbo.Construction.Steps.FindElement
{
    public interface IFindElementWithWebDriver
    {
        void Run(IWebDriver driver, object instance);
    }
}
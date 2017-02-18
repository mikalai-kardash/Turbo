using OpenQA.Selenium;

namespace Turbo.Construction.Steps.FindElement
{
    public interface IFindElementWithWebElement
    {
        void Run(IWebElement element, object instance);
    }
}
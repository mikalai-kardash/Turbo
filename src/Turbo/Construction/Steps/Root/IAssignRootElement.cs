using OpenQA.Selenium;

namespace Turbo.Construction.Steps.Root
{
    public interface IAssignRootElement
    {
        void Run(IWebElement root, object instance);
    }
}
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullTargetLocator : ITargetLocator
    {
        public IWebDriver Frame(int frameIndex)
        {
            throw new System.NotImplementedException();
        }

        public IWebDriver Frame(string frameName)
        {
            throw new System.NotImplementedException();
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            throw new System.NotImplementedException();
        }

        public IWebDriver ParentFrame()
        {
            throw new System.NotImplementedException();
        }

        public IWebDriver Window(string windowName)
        {
            throw new System.NotImplementedException();
        }

        public IWebDriver DefaultContent()
        {
            throw new System.NotImplementedException();
        }

        public IWebElement ActiveElement()
        {
            throw new System.NotImplementedException();
        }

        public IAlert Alert()
        {
            throw new System.NotImplementedException();
        }
    }
}
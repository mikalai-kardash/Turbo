using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Pages.Normal
{
    public class NormalPage
    {
        private IWebElement element;

        public Parts.Normal.Normal Normal { get; set; }

        public IWebElement GetElement()
        {
            return element;
        }
    }
}
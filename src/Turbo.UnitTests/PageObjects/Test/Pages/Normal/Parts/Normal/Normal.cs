using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Pages.Normal.Parts.Normal
{
    public class Normal
    {
        private IWebElement element;

        public Test.Parts.Simple.Simple Simple { get; set; }

        public IWebElement GetElement()
        {
            return element;
        }
    }
}
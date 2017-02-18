using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Parts.Normal
{
    public class Normal
    {
        private IWebElement element;

        public Simple.Simple Simple { get; set; }

        public IWebElement GetElement()
        {
            return element;
        }
    }
}
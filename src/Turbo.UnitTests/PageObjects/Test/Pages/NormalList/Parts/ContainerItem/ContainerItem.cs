using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Pages.NormalList.Parts.ContainerItem
{
    public class ContainerItem
    {
        private IWebElement title;
        private IWebElement description;
        private IWebElement image;

        public IWebElement GetTitle()
        {
            return title;
        }

        public IWebElement GetDescription()
        {
            return description;
        }

        public IWebElement GetImage()
        {
            return image;
        }
    }
}
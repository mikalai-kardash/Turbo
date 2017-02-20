using OpenQA.Selenium;
using Turbo.UnitTests.PageObjects.Test.Pages.NormalList.Parts.Container;

namespace Turbo.UnitTests.PageObjects.Test.Pages.NormalList
{
    public class NormalListPage
    {
        private IWebDriver browser;

        public Container Container { get; set; }
    }
}
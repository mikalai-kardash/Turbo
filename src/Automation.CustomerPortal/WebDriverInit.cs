using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Automation.CustomerPortal
{
    [Binding]
    public class WebDriverInit
    {
        private readonly IObjectContainer _container;

        public WebDriverInit(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void Init()
        {
            var driver = new ChromeDriver();
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }
    }
}
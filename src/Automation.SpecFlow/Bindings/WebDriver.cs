using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Automation.SpecFlow.Bindings
{
    [Binding]
    public class WebDriver
    {
        private readonly IObjectContainer _container;

        public WebDriver(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void Init()
        {
            var driver = new ChromeDriver();
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void Close()
        {
            _container.Resolve<IWebDriver>().Quit();
        }
    }
}
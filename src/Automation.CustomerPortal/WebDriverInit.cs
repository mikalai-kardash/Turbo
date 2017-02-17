using Automation.SpecFlow.Bindings;
using BoDi;
using TechTalk.SpecFlow;

namespace Automation.CustomerPortal
{
    [Binding]
    public class WebDriverInit : WebDriver
    {
        public WebDriverInit(IObjectContainer container)
            : base(container)
        {
        }
    }
}
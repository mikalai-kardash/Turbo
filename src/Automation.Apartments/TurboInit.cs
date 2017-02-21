using Automation.PageObjects.Apartments;
using BoDi;
using TechTalk.SpecFlow;
using Turbo;

namespace Automation.Apartments
{
    [Binding]
    public class TurboInit : SpecFlow.Bindings.Turbo
    {
        public TurboInit(IObjectContainer container) : base(container)
        {
        }

        [BeforeTestRun]
        public void ConfigureApp()
        {
            TurboInitializer.Scan(typeof(ApartmentsApp).Assembly);
        }
    }
}
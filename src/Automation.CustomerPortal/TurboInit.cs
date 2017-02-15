using BoDi;
using TechTalk.SpecFlow;
using Turbo;

namespace Automation.CustomerPortal
{
    [Binding]
    public class TurboInit
    {
        private readonly IObjectContainer _container;

        public TurboInit(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void Init()
        {
            var turbo = TurboInitializer.Init();
            _container.RegisterInstanceAs(turbo);
        }
    }
}
using BoDi;
using TechTalk.SpecFlow;
using Turbo;

namespace Automation.SpecFlow.Bindings
{
    [Binding]
    public class Turbo
    {
        private readonly IObjectContainer _container;

        public Turbo(IObjectContainer container)
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
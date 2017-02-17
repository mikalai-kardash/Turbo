using BoDi;
using TechTalk.SpecFlow;

namespace Automation.CustomerPortal
{
    [Binding]
    public class TurboInit : SpecFlow.Bindings.Turbo
    {
        public TurboInit(IObjectContainer container) : base(container)
        {
        }
    }
}
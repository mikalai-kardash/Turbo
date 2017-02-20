using OpenQA.Selenium;
using Turbo.Construction.Target;

namespace Turbo.Construction.Steps.WebDriver
{
    public class SetWebDriver : ISetWebDriver
    {
        private readonly ITarget _target;

        public SetWebDriver(ITarget target)
        {
            _target = target;
        }

        public void Run(IWebDriver driver, object instance)
        {
            _target.SetValue(instance, driver);
        }
    }
}
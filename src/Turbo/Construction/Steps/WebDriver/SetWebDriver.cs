using System.Reflection;
using OpenQA.Selenium;

namespace Turbo.Construction.Steps.WebDriver
{
    public class SetWebDriver : ISetWebDriver
    {
        private readonly FieldInfo _field;

        public SetWebDriver(FieldInfo field)
        {
            _field = field;
        }

        public void Run(IWebDriver driver, object instance)
        {
            _field.SetValue(instance, driver);
        }
    }
}
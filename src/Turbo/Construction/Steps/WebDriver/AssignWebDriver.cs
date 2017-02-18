using System.Reflection;
using OpenQA.Selenium;

namespace Turbo.Construction.Steps.WebDriver
{
    public class AssignWebDriver : ISetWebDriver
    {
        private readonly FieldInfo _field;

        public AssignWebDriver(FieldInfo field)
        {
            _field = field;
        }
        
        public void Run(IWebDriver driver, object instance)
        {
            _field.SetValue(instance, driver);
        }
    }
}
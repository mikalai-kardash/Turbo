using System.Reflection;
using OpenQA.Selenium;

namespace Turbo.Construction.Steps.Root
{
    public class AssignRootElement : IAssignRootElement
    {
        private readonly PropertyInfo _property;
        private readonly FieldInfo _field;

        public AssignRootElement(FieldInfo field)
        {
            _field = field;
        }

        public AssignRootElement(PropertyInfo property)
        {
            _property = property;
        }

        public void Run(IWebElement root, object instance)
        {
            if (_field != null)
            {
                _field.SetValue(instance, root);
            }
            else
            {
                _property.SetValue(instance, root);
            }
        }
    }
}
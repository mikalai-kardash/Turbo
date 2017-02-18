using System;
using System.Reflection;
using OpenQA.Selenium;

namespace Turbo.Construction.Steps.AssignPart
{
    public class AssignPart : IAssignPart
    {
        private readonly FieldInfo _field;
        private readonly PartInfo _partInfo;

        public AssignPart(FieldInfo field, PartInfo partInfo)
        {
            _field = field;
            _partInfo = partInfo;
        }

        public void Run(IWebDriver driver, object instance, Func<Type, object> createInstance)
        {
            var part = _partInfo.Analysis.Activate(driver, createInstance);
            _field.SetValue(instance, part);
        }

        public void Run(IWebDriver driver, IWebElement parent, object instance, Func<Type, object> createInstance)
        {
            var part = _partInfo.Analysis.Activate(driver, parent, createInstance);
            _field.SetValue(instance, part);
        }
    }
}
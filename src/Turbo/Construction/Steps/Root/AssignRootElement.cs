using OpenQA.Selenium;
using Turbo.Construction.Target;

namespace Turbo.Construction.Steps.Root
{
    public class AssignRootElement : IAssignRootElement
    {
        private readonly ITarget _target;

        public AssignRootElement(ITarget target)
        {
            _target = target;
        }

        public void Run(IWebElement root, object instance)
        {
            _target.SetValue(instance, root);
        }
    }
}
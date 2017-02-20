using Turbo.Cache.Info;
using Turbo.Construction.Context;
using Turbo.Construction.Target;

namespace Turbo.Construction.Steps.AssignPart
{
    public class AssignPart : IAssignPart
    {
        private readonly PartInfo _partInfo;
        private readonly ITarget _target;

        public AssignPart(ITarget target, PartInfo partInfo)
        {
            _target = target;
            _partInfo = partInfo;
        }

        public void Run(InstanceContext context)
        {
            var part = _partInfo.Analysis.Activate(context.ToExecution());
            _target.SetValue(context.Instance, part);
        }
    }
}
using System.Reflection;
using Turbo.Cache.Info;
using Turbo.Construction.Context;

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

        public void Run(InstanceContext context)
        {
            var part = _partInfo.Analysis.Activate(context.ToExecution());
            _field.SetValue(context.Instance, part);
        }
    }
}
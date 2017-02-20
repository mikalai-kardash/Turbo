using System;
using System.Collections.Generic;
using System.Linq;
using Turbo.Cache.Info;
using Turbo.Construction.Context;
using Turbo.Construction.Target;

namespace Turbo.Construction.Steps.AssignPart
{
    public class AssignPartCollection : IAssignPart
    {
        private readonly PartInfo _partInfo;
        private readonly ITarget _target;

        public AssignPartCollection(ITarget target, PartInfo partInfo)
        {
            _target = target;
            _partInfo = partInfo;
        }

        public void Run(InstanceContext context)
        {
            var parts = _partInfo
                .Analysis
                .ActivateCollection(context.ToExecution())
                .ToList();

            _target.SetValue(context.Instance, ToArray(parts));
        }

        private Array ToArray(IReadOnlyList<object> parts)
        {
            var partsType = _target.GetTypeOfArray();
            var array = Array.CreateInstance(partsType, parts.Count);

            for (var i = 0; i < parts.Count; i++)
            {
                array.SetValue(parts[i], i);
            }

            return array;
        }
    }
}
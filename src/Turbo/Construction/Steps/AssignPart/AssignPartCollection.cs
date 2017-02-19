using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Turbo.Cache.Info;
using Turbo.Construction.Context;

namespace Turbo.Construction.Steps.AssignPart
{
    public class AssignPartCollection : IAssignPart
    {
        private readonly FieldInfo _field;
        private readonly PartInfo _partInfo;

        public AssignPartCollection(FieldInfo field, PartInfo partInfo)
        {
            _field = field;
            _partInfo = partInfo;
        }

        public void Run(InstanceContext context)
        {
            var parts = _partInfo
                .Analysis
                .ActivateCollection(context.ToExecution())
                .ToList();

            SetValue(context.Instance, parts);
        }

        private void SetValue(object instance, IReadOnlyList<object> part)
        {
            _field.SetValue(instance, ToArray(part));
        }

        private Array ToArray(IReadOnlyList<object> parts)
        {
            var partsType = _field.FieldType.GetElementType();
            var array = Array.CreateInstance(partsType, parts.Count);

            for (var i = 0; i < parts.Count; i++)
            {
                array.SetValue(parts[i], i);
            }

            return array;
        }
    }
}
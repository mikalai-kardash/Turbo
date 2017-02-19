using System;
using System.Collections.Generic;
using System.Reflection;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.DI;

namespace Turbo.Construction
{
    public class PartAnalyzer : IPageBuilder, IPartAnalyzer
    {
        private readonly IObjectFactory _objectFactory;
        private readonly IInfoProvider _infoProvider;

        private PartInfo _part;

        private static readonly HashSet<string> RootNames =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "root",
                "_root",
                "container",
                "_container"
            };

        public PartAnalyzer(IObjectFactory objectFactory, IInfoProvider infoProvider)
        {
            _objectFactory = objectFactory;
            _infoProvider = infoProvider;
        }

        public PartInfo Analyze(Type partType)
        {
            _part = _infoProvider.GetPartInfo(partType);

            if (!_part.Analysis.IsDone)
            {
                TypeAnalyzer.Analyze(partType, this);
            }

            return _part;
        }

        public void SetWebElement(FieldInfo field)
        {
            var fieldName = field.Name;
            if (RootNames.Contains(fieldName))
            {
                _part.Analysis.AssignRoot(field);
                return;
            }

            var selector = _part.Finder.GetSelector(fieldName);
            _part.Analysis.Assign(field, selector);
        }

        public void SetWebElement(PropertyInfo property)
        {
            var propertyName = property.Name;
            if (RootNames.Contains(propertyName))
            {
                _part.Analysis.AssignRoot(property);
                return;
            }

            var selector = _part.Finder.GetSelector(propertyName);
            _part.Analysis.Assign(property, selector);
        }

        public void SetWebDriver(FieldInfo field)
        {
            _part.Analysis.AssignWithWebDriver(field);
        }

        public void SetPart(PropertyInfo property)
        {
        }

        public void SetPart(FieldInfo field)
        {
            var partType = field.FieldType;
            var partAnalyzer = _objectFactory.GetInstance<IPartAnalyzer>();
            var partInfo = partAnalyzer.Analyze(partType);

            _part.Analysis.AssignPart(field, partInfo);
        }

        public void SetPartCollection(FieldInfo field)
        {
        }
    }
}
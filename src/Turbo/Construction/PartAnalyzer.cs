using System;
using System.Collections.Generic;
using System.Reflection;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;

namespace Turbo.Construction
{
    public class PartAnalyzer : IPageBuilder
    {
        private readonly PartInfo _parent;
        private readonly IMetadataLoader _metadataLoader = new YamlMetadataLoader();
        private readonly IObjectFactory _objectFactory;
        private readonly Type _partType;
        private PartInfo _part;

        private static readonly HashSet<string> RootNames =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "root",
                "_root",
                "container",
                "_container"
            };

        public PartAnalyzer(IObjectFactory objectFactory, Type partType)
        {
            _objectFactory = objectFactory;
            _partType = partType;
        }

        private PartAnalyzer(IObjectFactory objectFactory, Type partType, PartInfo parent)
            : this(objectFactory, partType)
        {
            _parent = parent;
        }

        public PartInfo Analyze()
        {
            _part = GetPartInfo(_partType);

            if (!_part.Analysis.IsDone)
            {
                var rootSelector = _part.Part.Meta.Selector;
                if (!string.IsNullOrWhiteSpace(rootSelector))
                {
                    _part.Analysis.AssignRootElement(rootSelector, _parent?.Analysis);
                }

                TypeAnalyzer.Analyze(_partType, this);
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
            var partInfo = new PartAnalyzer(_objectFactory, partType, _part).Analyze();

            _part.Analysis.AssignPart(field, partInfo);
        }

        public void SetPartCollection(FieldInfo field)
        {
            throw new NotImplementedException();
        }

        private PartInfo GetPartInfo(Type partType)
        {
            PartInfo partInfo;
            if (!TurboSync.TryGetPart(partType, out partInfo))
            {
                var meta = _metadataLoader.GetPartMeta(partType);
                partInfo = TurboSync.AddPart(meta);
            }
            return partInfo;
        }
    }
}
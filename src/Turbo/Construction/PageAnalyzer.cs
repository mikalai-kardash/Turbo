using System;
using System.Reflection;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.DI;

namespace Turbo.Construction
{
    public class PageAnalyzer : IPageBuilder, IPageAnalyzer
    {
        private readonly IObjectFactory _objectFactory;
        private readonly IInfoProvider _infoProvider;

        private PageInfo _page;

        public PageAnalyzer(IObjectFactory objectFactory, IInfoProvider infoProvider)
        {
            _objectFactory = objectFactory;
            _infoProvider = infoProvider;
        }

        public PageInfo Analyze(Type pageType)
        {
            _page = _infoProvider.GetPageInfo(pageType);
            if (!_page.Analysis.IsDone)
            {
                TypeAnalyzer.Analyze(pageType, this);
            }
            return _page;
        }

        public void SetWebElement(FieldInfo field)
        {
            var fieldName = field.Name;
            var selector = _page.Finder.GetSelector(fieldName);
            _page.Analysis.Assign(field, selector);
        }

        public void SetWebElement(PropertyInfo property)
        {
            var propertyName = property.Name;
            var selector = _page.Finder.GetSelector(propertyName);
            _page.Analysis.Assign(property, selector);
        }

        public void SetWebDriver(FieldInfo field)
        {
            _page.Analysis.AssignWithWebDriver(field);
        }

        public void SetPart(PropertyInfo property)
        {
        }

        public void SetPart(FieldInfo field)
        {
            var partType = field.FieldType;
            var analyzer = _objectFactory.GetInstance<IPartAnalyzer>();
            var partInfo = analyzer.Analyze(partType);

            _page.Analysis.AssignPart(field, partInfo);
        }

        public void SetPartCollection(FieldInfo field)
        {
            var partType = field.FieldType.GetElementType();
            var analyzer = _objectFactory.GetInstance<IPartAnalyzer>();
            var partInfo = analyzer.Analyze(partType);

            _page.Analysis.AssignPartCollection(field, partInfo);
        }
    }
}
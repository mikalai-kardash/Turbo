using System;
using System.Reflection;

namespace Turbo.Construction
{
    public class PageAnalyzer<T> : IPageBuilder, IPageAnalyzer
    {
        private readonly IObjectFactory _objectFactory;
        private readonly PageInfo _page;

        public PageAnalyzer(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
            TurboSync.TryGetPage<T>(out _page);
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
            var partInfo = new PartAnalyzer(_objectFactory, partType).Analyze();

            _page.Analysis.AssignPart(field, partInfo);
        }

        public void SetPartCollection(FieldInfo field)
        {
            var partType = field.FieldType.GetElementType();
            var partInfo = new PartAnalyzer(_objectFactory, partType).Analyze();

            _page.Analysis.AssignPartCollection(field, partInfo);
        }

        public PageInfo Analyze()
        {
            var pageInfo = GetPageInfo(typeof(T));
            if (!pageInfo.Analysis.IsDone)
            {
                TypeAnalyzer.Analyze<T>(this);
            }
            return pageInfo;
        }

        private static PageInfo GetPageInfo(Type pageType)
        {
            PageInfo pageInfo;
            TurboSync.TryGetPage(pageType, out pageInfo);
            return pageInfo;
        }
    }
}
using System.Reflection;
using OpenQA.Selenium;

namespace Turbo
{
    public class PageBuilder
    {
        private readonly IWebDriver _driver;
        private readonly IObjectFactory _objectFactory;

        private PageInfo _currentPage;

        public PageBuilder(IWebDriver driver, IObjectFactory objectFactory)
        {
            _driver = driver;
            _objectFactory = objectFactory;
        }

        public void NavigateToPageFirst()
        {
        }

        public void SetWebElement(FieldInfo field)
        {
            var selector = _currentPage.Finder.GetSelector(field.Name);
            _currentPage.Analysis.Assign(field, selector);
        }

        public void SetWebElement(PropertyInfo property)
        {
        }

        public void SetWebDriver(FieldInfo field)
        {
            _currentPage.Analysis.AssignWithWebDriver(field);
        }

        public void SetWebDriver(PropertyInfo property)
        {
        }

        public void SetPagePart(PropertyInfo property)
        {
        }

        public void SetPagePart(FieldInfo field)
        {
        }


        public TPage Build<TPage>()
        {
            TurboSync.TryGetPage<TPage>(out _currentPage);
            if (!_currentPage.Analysis.IsDone)
            {
                TypeAnalyzer.Analyze<TPage>(this);
            }

            var page = _objectFactory.GetInstance<TPage>();
            _currentPage.Analysis.Activate(page, _driver);
            return page;
        }
    }
}
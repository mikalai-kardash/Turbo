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
            _currentPage.AddField(field);
        }

        public void SetWebElement(PropertyInfo property)
        {
            _currentPage.AddProperty(property);
        }

        public void SetWebDriver(FieldInfo field)
        {
        }

        public void SetWebDriver(PropertyInfo property)
        {
        }

        public void SetPagePart(PropertyInfo property)
        {
            TypeAnalyzer.Analyze(property.PropertyType, this);
        }

        public void SetPagePart(FieldInfo field)
        {
            TypeAnalyzer.Analyze(field.FieldType, this);
        }


        public TPage Build<TPage>()
        {
            TurboSync.TryGetPage<TPage>(out _currentPage);

            TypeAnalyzer.Analyze<TPage>(this);



            return _objectFactory.GetInstance<TPage>();
        }
    }
}
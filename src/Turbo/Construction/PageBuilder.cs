using OpenQA.Selenium;

namespace Turbo.Construction
{
    public class PageBuilder : IPageFactory
    {
        private readonly IWebDriver _driver;
        private readonly IObjectFactory _objectFactory;

        public PageBuilder(IWebDriver driver, IObjectFactory objectFactory)
        {
            _driver = driver;
            _objectFactory = objectFactory;
        }

        public void NavigateToPageFirst()
        {
        }

        public TPage Build<TPage>()
        {
            var pageInfo = GetPageInfo<TPage>();
            return (TPage)pageInfo.Analysis.Activate(
                _driver,
                _objectFactory.GetInstance);
        }

        private PageInfo GetPageInfo<TPage>()
        {
            PageInfo pageInfo;
            TurboSync.TryGetPage<TPage>(out pageInfo);


            if (!pageInfo.Analysis.IsDone)
            {
                new PageAnalyzer<TPage>(_objectFactory).Analyze();
            }

            return pageInfo;
        }
    }
}
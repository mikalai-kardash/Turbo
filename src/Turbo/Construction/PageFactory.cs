using System;
using OpenQA.Selenium;
using Turbo.Construction.Context;
using Turbo.DI;

namespace Turbo.Construction
{
    public class PageFactory : IPageFactory
    {
        private readonly IObjectFactory _objectFactory;

        public PageFactory(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public void NavigateToPageFirst()
        {
        }

        public TPage Build<TPage>(IWebDriver driver)
        {
            var pageType = typeof(TPage);
            return (TPage) BuildPage(driver, pageType);
        }

        private object BuildPage(IWebDriver driver, Type pageType)
        {
            var analyzer = _objectFactory.GetInstance<IPageAnalyzer>();
            var pageInfo = analyzer.Analyze(pageType);

            return pageInfo.Analysis.Activate(new ExecutionContext
            {
                Driver = driver,
                Factory = _objectFactory.GetInstance
            });
        }
    }
}
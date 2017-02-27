using System;
using System.ComponentModel;
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

        public T BuildPage<T>(IWebDriver driver)
        {
            return (T) BuildPage(driver, typeof(T));
        }

        public T BuildPart<T>(IWebDriver driver)
        {
            return (T) BuildPart(driver, null, typeof(T));
        }

        public T BuildPart<T>(IWebDriver driver, IWebElement parent)
        {
            return (T) BuildPart(driver, parent, typeof(T));
        }

        private object BuildPart(IWebDriver driver, IWebElement parent, Type type)
        {
            var analyzer = _objectFactory.GetInstance<IPartAnalyzer>();
            var partInfo = analyzer.Analyze(type);
            var analysis = partInfo.Analysis;

            return analysis.Activate(new ExecutionContext
            {
                Driver = driver,
                Parent = parent,
                Factory = _objectFactory.GetInstance
            });
        }

        private object BuildPage(IWebDriver driver, Type pageType)
        {
            var analyzer = _objectFactory.GetInstance<IPageAnalyzer>();
            var pageInfo = analyzer.Analyze(pageType);
            var analysis = pageInfo.Analysis;

            return analysis.Activate(new ExecutionContext
            {
                Driver = driver,
                Factory = _objectFactory.GetInstance
            });
        }
    }
}
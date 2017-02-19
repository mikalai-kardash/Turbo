using System;
using OpenQA.Selenium;
using Turbo.Cache;
using Turbo.Construction;
using Turbo.DI;

namespace Turbo
{
    internal class TurboFactory : ITurboFactory
    {
        private readonly IObjectFactory _objectFactory;
        private readonly IInfoProvider _infoProvider;

        public TurboFactory(IObjectFactory objectFactory, IInfoProvider infoProvider)
        {
            _objectFactory = objectFactory;
            _infoProvider = infoProvider;
        }

        public TPage GetPage<TApp, TPage>(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var appInfo = _infoProvider.GetAppInfo<TApp>();
            var pageInfo = _infoProvider.GetPageInfo<TPage>(appInfo);

            var pageUrl = pageInfo.GetPageUrl();
            var browserUrl = driver.Url;

            var pageBuilder = _objectFactory.GetInstance<IPageFactory>();
            if (!pageUrl.Equals(browserUrl, StringComparison.OrdinalIgnoreCase))
            {
                pageBuilder.NavigateToPageFirst();
            }

            return pageBuilder.Build<TPage>(driver);
        }
    }
}
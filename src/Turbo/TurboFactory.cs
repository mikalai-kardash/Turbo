using System;
using OpenQA.Selenium;
using Turbo.Construction;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;

namespace Turbo
{
    public class TurboFactory
    {
        private readonly TurboConfiguration _configuration;
        private readonly IMetadataLoader _metadataLoader; // todo: candidate for configuration

        public TurboFactory(TurboConfiguration configuration)
        {
            _configuration = configuration;
            _metadataLoader = new YamlMetadataLoader();
        }

        public TPage GetPage<TApp, TPage>(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var appInfo = GetAppInfo<TApp>();
            var pageInfo = GetPageInfo<TPage>(appInfo);

            var pageBuilder = new PageBuilder(driver, _configuration.ObjectFactory);

            var pageUrl = pageInfo.GetPageUrl();
            var browserUrl = driver.Url;

            if (string.IsNullOrWhiteSpace(browserUrl))
            {
                pageBuilder.NavigateToPageFirst();
            }
            else
            {
                var url = new Uri(browserUrl);
                if (!url.AbsolutePath.Equals(pageUrl, StringComparison.OrdinalIgnoreCase))
                {
                    pageBuilder.NavigateToPageFirst();
                }
            }

            return pageBuilder.Build<TPage>();
        }

        private PageInfo GetPageInfo<TPage>(AppInfo appInfo)
        {
            PageInfo pageInfo;
            if (TurboSync.TryGetPage<TPage>(out pageInfo))
            {
                return pageInfo;
            }

            var pageMeta = _metadataLoader.GetPageMeta<TPage>();
            pageInfo = TurboSync.AddPage(appInfo.App, pageMeta);
            return pageInfo;
        }

        private AppInfo GetAppInfo<TApp>()
        {
            AppInfo appInfo;
            if (TurboSync.TryGetApp<TApp>(out appInfo))
            {
                return appInfo;
            }

            var appMeta = _metadataLoader.GetAppMeta<TApp>();
            appInfo = TurboSync.AddApp(appMeta);
            return appInfo;
        }
    }
}
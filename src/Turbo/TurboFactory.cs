using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using Turbo.Metadata;
using Turbo.Metadata.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Turbo
{
    public class TurboFactory
    {
        private readonly TurboConfiguration _configuration;

        public TurboFactory(TurboConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TPage GetPage<TApp, TPage>(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            AppInfo appInfo;
            if (!TurboSync.TryGetApp<TApp>(out appInfo))
            {
                var appMeta = GetAppMeta<TApp>();
                appInfo = TurboSync.AddApp(appMeta);
            }

            PageInfo pageInfo;
            if (!TurboSync.TryGetPage<TPage>(out pageInfo))
            {
                var pageMeta = GetPageMeta<TPage>();
                pageInfo = TurboSync.AddPage(appInfo.App, pageMeta);
            }

            var pageBuilder = new PageBuilder(driver, _configuration.ObjectFactory);

            var pageUrl = pageInfo.GetPageUrl();

            var actualUrl = driver.Url;
            if (string.IsNullOrWhiteSpace(actualUrl))
            {
                pageBuilder.NavigateToPageFirst();
            }
            else
            {
                var url = new Uri(actualUrl);
                if (!url.AbsolutePath.Equals(pageUrl, StringComparison.OrdinalIgnoreCase))
                {
                    pageBuilder.NavigateToPageFirst();
                }
            }

            return pageBuilder.Build<TPage>();
        }

        private static TMeta GetMetadata<TMeta, T>()
        {
            var metadata = default(TMeta);

            var type = typeof(T);
            var assembly = type.Assembly;
            var resource = $"{type.FullName}.yaml";

            var resources = new HashSet<string>(assembly.GetManifestResourceNames());

            if (resources.Contains(resource))
            {
                var stream = assembly.GetManifestResourceStream(resource);
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var deserializer = new DeserializerBuilder()
                            .WithNamingConvention(new CamelCaseNamingConvention())
                            .Build();

                        metadata = deserializer.Deserialize<TMeta>(reader);
                    }
                }
            }

            return metadata;
        }

        private static AppMeta<T> GetAppMeta<T>()
        {
            var meta = GetMetadata<App, T>();
            return new AppMeta<T>(meta);
        }

        private static PageMeta<T> GetPageMeta<T>()
        {
            var meta = GetMetadata<Page, T>();
            return new PageMeta<T>(meta);
        }
    }
}
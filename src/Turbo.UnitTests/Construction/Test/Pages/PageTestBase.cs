using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.Construction;
using Turbo.DI;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages
{
    [TestClass]
    public class PageTestBase
    {
        private IMetadataLoader _loader;

        [TestInitialize]
        public void InitBase()
        {
            _loader = new YamlMetadataLoader();
            WebDriver = new NullDriver();

            var factory = new SimpleObjectFactory();
            TurboInitializer.RegisterBuiltInTypes(factory);
            factory.Instance(_loader);

            PageBuilder = factory.GetInstance<IPageFactory>();
        }

        public IPageFactory PageBuilder { get; private set; }

        public NullDriver WebDriver { get; private set; }

        protected AppInfo RegisterApp<TApp>()
        {
            AppInfo appInfo;
            if (!TurboSync.TryGetApp(typeof(TApp), out appInfo))
            {
                var appMeta = _loader.GetAppMeta<TApp>();
                appInfo = TurboSync.AddApp(appMeta);
            }

            return appInfo;
        }

        protected PageInfo RegisterPage<TPage>(AppInfo appInfo)
        {
            PageInfo pageInfo;
            if (!TurboSync.TryGetPage(typeof(TPage), out pageInfo))
            {
                var page = _loader.GetPageMeta<TPage>();
                pageInfo = TurboSync.AddPage(appInfo.App, page);
            }

            return pageInfo;
        }

        protected T GetPage<T>()
        {
            return PageBuilder.BuildPage<T>(WebDriver);
        }

        protected T GetPart<T>()
        {
            return PageBuilder.BuildPart<T>(WebDriver);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty2;
using Turbo.UnitTests.PageObjects.Test.Pages.Fields;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests
{
    [TestClass]
    public class PageBuilderTests
    {
        private PageBuilder _pageBuilder;
        private YamlMetadataLoader _loader;

        [TestInitialize]
        public void SetUp()
        {
            _loader = new YamlMetadataLoader();
            _pageBuilder = new PageBuilder(
                new NullDriver(), new DefaultObjectFactory());
            
            var appInfo = RegisterApp<TestApp>();
            RegisterPage<EmptyPage>(appInfo);
            RegisterPage<Empty2Page>(appInfo);
            RegisterPage<FieldsPage>(appInfo);
        }

        private AppInfo RegisterApp<TApp>()
        {
            AppInfo appInfo;
            if (!TurboSync.TryGetApp(typeof(TApp), out appInfo))
            {
                var appMeta = _loader.GetAppMeta<TApp>();
                appInfo = TurboSync.AddApp(appMeta);
            }

            return appInfo;
        }

        private PageInfo RegisterPage<TPage>(AppInfo appInfo)
        {
            PageInfo pageInfo;
            if (!TurboSync.TryGetPage(typeof(TPage), out pageInfo))
            {
                var page = _loader.GetPageMeta<TPage>();
                pageInfo = TurboSync.AddPage(appInfo.App, page);
            }

            return pageInfo;
        }

        [TestMethod]
        public void Creates_empty_page()
        {
            var page = _pageBuilder.Build<EmptyPage>();
            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void Creates_empty2_page()
        {
            var page = _pageBuilder.Build<Empty2Page>();
            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void Creates_fields_page()
        {
            var page = _pageBuilder.Build<FieldsPage>();

            Assert.IsNotNull(page.GetBrowser(), "browser");
            Assert.IsNotNull(page.Browser, "Browser");

            Assert.IsNotNull(page.Get_element(), "_element");
        }
    }
}
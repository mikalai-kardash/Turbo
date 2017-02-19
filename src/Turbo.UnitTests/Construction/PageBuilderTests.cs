using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.Construction;
using Turbo.DI;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty2;
using Turbo.UnitTests.PageObjects.Test.Pages.Fields;
using Turbo.UnitTests.PageObjects.Test.Pages.HasPart;
using Turbo.UnitTests.PageObjects.Test.Pages.Normal;
using Turbo.UnitTests.PageObjects.Test.Pages.Properties;
using Turbo.UnitTests.PageObjects.Test.Pages.SimpleList;
using Turbo.UnitTests.PageObjects.Test.Parts.Simple;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction
{
    [TestClass]
    public class PageBuilderTests
    {
        private IPageFactory _pageBuilder;
        private YamlMetadataLoader _loader;
        private NullDriver _nullDriver;

        [TestInitialize]
        public void SetUp()
        {
            _loader = new YamlMetadataLoader();
            _nullDriver = new NullDriver();

            var factory = new DefaultObjectFactory();
            TurboInitializer.RegisterBuiltInTypes(factory);
            factory.RegisterInstance<IMetadataLoader>(_loader);

            _pageBuilder = factory.GetInstance<IPageFactory>();
            
            var appInfo = RegisterApp<TestApp>();

            RegisterPage<EmptyPage>(appInfo);
            RegisterPage<Empty2Page>(appInfo);
            RegisterPage<FieldsPage>(appInfo);
            RegisterPage<PropertiesPage>(appInfo);
            RegisterPage<HasPartPage>(appInfo);
            RegisterPage<NormalPage>(appInfo);
            RegisterPage<SimpleListPage>(appInfo);
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
            var page = _pageBuilder.Build<EmptyPage>(_nullDriver);
            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void Creates_empty2_page()
        {
            var page = _pageBuilder.Build<Empty2Page>(_nullDriver);
            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void Creates_fields_page()
        {
            var page = _pageBuilder.Build<FieldsPage>(_nullDriver);

            Assert.IsNotNull(page.GetBrowser(), "browser");
            Assert.IsNotNull(page.Browser, "Browser");
            Assert.IsNotNull(page.Get_element(), "_element");

            Assert.IsNull(page.GetNotInjected(), "notInjected");
            Assert.IsNull(page._notInjected, "_notInjected");
        }

        [TestMethod]
        public void Created_properties_page()
        {
            var page = _pageBuilder.Build<PropertiesPage>(_nullDriver);

            Assert.IsNotNull(page.Browser, "Browser");
            Assert.IsNotNull(page.GetPrivateBrowser(), "PrivateBrowser");
            Assert.IsNull(page.ReadOnlyBrowser, "ReadOnlyBrowser");

            Assert.IsNotNull(page.Element, "Element");
            Assert.IsNotNull(page.GetElement(), "(private) Element");
            Assert.IsNull(page._Element, "ReadOnlyElement");
        }

        [TestMethod]
        public void Creates_parts_page()
        {
            _nullDriver.ExpectFindElements(new []
            {
                new NullElement()
            });

            var page = _pageBuilder.Build<HasPartPage>(_nullDriver);

            VerifySimple(page.Simple);
        }

        [TestMethod]
        public void Creates_normal_page()
        {
            var normalElement = new NullElement();
            _nullDriver.ExpectFindElements(new[]
            {
                normalElement
            });
            normalElement.ExpectFindElements(new []
            {
                new NullElement()
            });
            
            var page = _pageBuilder.Build<NormalPage>(_nullDriver);

            Assert.IsNotNull(page.GetElement());

            var normal = page.Normal;

            Assert.IsNotNull(normal, "normal");
            Assert.IsNotNull(normal.GetElement());

            VerifySimple(normal.Simple);
        }

        [TestMethod]
        public void Creates_simple_list_page()
        {
            _nullDriver.ExpectFindElements(new[]
            {
                new NullElement(),
                new NullElement(),
                new NullElement(),
            });

            var page = _pageBuilder.Build<SimpleListPage>(_nullDriver);
            Assert.IsNotNull(page.Items, "Items[]");
        }

        private static void VerifySimple(Simple simple)
        {
            Assert.IsNotNull(simple, "Simple");

            // Field - WebDriver
            Assert.IsNotNull(simple.GetBrowser(), "Browser");

            // Field - Root
            Assert.IsNotNull(simple.GetRoot(), "root");
            Assert.IsNotNull(simple.Get_Root(), "_root");
            Assert.IsNotNull(simple.GetContainer(), "container");
            Assert.IsNotNull(simple.Get_Container(), "_container");

            // Field - Element
            Assert.IsNotNull(simple.GetElement(), "element");

            // Property - WebDriver
            Assert.IsNotNull(simple.Browser, "Browser (property)");

            // Property - Root
            Assert.IsNotNull(simple.Root, "Root");
            Assert.IsNotNull(simple._Root, "_Root");
            Assert.IsNotNull(simple.GetROot(), "rOot");

            // Property - Element
            Assert.IsNotNull(simple.Element, "Element");
        }
    }
}
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.UnitTests.PageObjects.Google;
using Turbo.UnitTests.PageObjects.Google.Pages.Search;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Fields;
using Turbo.UnitTests.PageObjects.Test.Pages.HasPart;
using Turbo.UnitTests.PageObjects.Test.Pages.Normal;
using Turbo.UnitTests.PageObjects.Test.Pages.NormalList;
using Turbo.UnitTests.PageObjects.Test.Pages.Properties;
using Turbo.UnitTests.PageObjects.Test.Pages.SimpleList;

namespace Turbo.UnitTests
{
    [TestClass]
    public class TurboInitializerTests
    {
        [TestMethod]
        public void Adds_app()
        {
            TurboInitializer.AddApp<TestApp>();

            AppInfo app;
            Assert.IsTrue(TurboSync.TryGetApp<TestApp>(out app));
            Assert.IsNotNull(app.App);
            Assert.IsNotNull(app.App.Meta);
            Assert.AreEqual(typeof(TestApp), app.App.Type);
        }

        [TestMethod]
        public void Adds_page()
        {
            TurboInitializer.AddApp<TestApp>();
            TurboInitializer.AddPage<TestApp, NormalPage>();

            PageInfo page;
            Assert.IsTrue(TurboSync.TryGetPage(typeof(NormalPage), out page));

            Assert.IsNotNull(page.App);
            Assert.IsNotNull(page.Page);
            Assert.IsNotNull(page.Analysis);
            Assert.IsNotNull(page.Finder);

            Assert.IsTrue(page.Analysis.IsDone);
        }

        [TestMethod]
        public void Sync_loads_all_apps_and_pages()
        {
            TurboInitializer.Scan(GetType().Assembly);

            AppInfo testApp;
            Assert.IsTrue(GetApp<TestApp>(out testApp));

            AssertPage<FieldsPage>(testApp);
            AssertPage<HasPartPage>(testApp);
            AssertPage<NormalPage>(testApp);
            AssertPage<NormalListPage>(testApp);
            AssertPage<PropertiesPage>(testApp);
            AssertPage<SimpleListPage>(testApp);

            AppInfo googleApp;
            Assert.IsTrue(GetApp<GoogleSearchApp>(out googleApp));

            AssertPage<SearchPage>(googleApp);
        }

        private static void AssertPage<T>(AppInfo testApp)
        {
            PageInfo pageInfo;
            Assert.IsTrue(TurboSync.TryGetPage<T>(out pageInfo));

            Assert.AreSame(pageInfo.App, testApp.App);
            Assert.IsTrue(testApp.Pages.Contains(pageInfo.Page));

            Assert.IsTrue(pageInfo.Analysis.IsDone,
                $"Analysis is not done for {pageInfo.Page.Type} at {testApp.App.Type}");
        }

        private static bool GetApp<T>(out AppInfo app)
        {
            return TurboSync.TryGetApp<T>(out app);
        }
    }
}
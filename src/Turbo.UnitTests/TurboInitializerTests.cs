using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty;
using Turbo.UnitTests.PageObjects.Test.Pages.Normal;

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
    }
}
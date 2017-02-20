using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Properties;

namespace Turbo.UnitTests.Construction.Test.Pages.Properties
{
    [TestClass]
    public class PropertiesPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<PropertiesPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            var page = GetPage<PropertiesPage>();

            Assert.IsNotNull(page.Browser, "Browser");
            Assert.IsNotNull(page.GetPrivateBrowser(), "PrivateBrowser");
            Assert.IsNull(page.ReadOnlyBrowser, "ReadOnlyBrowser");

            Assert.IsNotNull(page.Element, "Element");
            Assert.IsNotNull(page.GetElement(), "(private) Element");
            Assert.IsNull(page._Element, "ReadOnlyElement");

            Assert.IsNotNull(page.Links, "Links");
        }
    }
}
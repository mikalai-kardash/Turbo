using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Fields;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.Fields
{
    [TestClass]
    public class FieldsPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<FieldsPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            WebDriver.Expect("#byId", new NullElement());
            WebDriver.Expect("a", new NullElement(), new NullElement());

            var page = GetPage<FieldsPage>();

            Assert.IsNotNull(page.GetBrowser(), "browser");
            Assert.IsNotNull(page.Browser, "Browser");
            Assert.IsNotNull(page.Get_element(), "_element");

            Assert.IsNull(page.GetNotInjected(), "notInjected");
            Assert.IsNull(page._notInjected, "_notInjected");

            Assert.IsNotNull(page.GetLinks(), "links");
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.SimpleList;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.SimpleList
{
    [TestClass]
    public class SimpleListPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<SimpleListPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            WebDriver.Expect(
                "article.item",
                new NullElement(),
                new NullElement(),
                new NullElement());

            var page = GetPage<SimpleListPage>();
            Assert.IsNotNull(page.Items, "Items[]");
        }
    }
}
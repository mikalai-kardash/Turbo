using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.NormalList;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.NormalList
{
    [TestClass]
    public class NormalListPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<NormalListPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            var widgets = new NullElement();

            WebDriver.Expect("#widgets", widgets);

            var items = new[] {
                new NullElement(),
                new NullElement(),
                new NullElement()
            };
            widgets.Expect(".widget", items);

            foreach (var item in items)
            {
                item.Expect("h3", new NullElement());
                item.Expect("p", new NullElement());
                item.Expect("div.image", new NullElement());
            }

            var page = GetPage<NormalListPage>();

            Assert.IsNotNull(page);
            Assert.IsNotNull(page.Container);
            Assert.IsNotNull(page.Container.Items);

            foreach (var item in page.Container.Items)
            {
                Assert.IsNotNull(item.GetTitle());
                Assert.IsNotNull(item.GetDescription());
                Assert.IsNotNull(item.GetImage());
            }
        }
    }
}
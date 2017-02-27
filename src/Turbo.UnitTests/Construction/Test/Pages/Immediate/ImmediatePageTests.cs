using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Immediate;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.Immediate
{
    [TestClass]
    public class ImmediatePageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<ImmediatePage>(app);
        }

        [TestMethod]
        public void Does_not_load_part_by_default()
        {
            WebDriver.Expect("#id", new NullElement());

            var page = GetPage<ImmediatePage>();

            Assert.IsNotNull(page);
            Assert.IsNull(page.Immediate);
        }

        [TestMethod]
        public void Has_factory_method_to_load_part_later()
        {
            WebDriver.Expect("#id", new NullElement());

            var page = GetPage<ImmediatePage>();

            page.Immediate = GetPart<PageObjects.Test.Pages.Immediate.Parts.Immediate.Immediate>();

            Assert.IsNotNull(page);
            Assert.IsNotNull(page.Immediate);
        }
    }
}
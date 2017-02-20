using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty2;

namespace Turbo.UnitTests.Construction.Test.Pages.Empty2
{
    [TestClass]
    public class Empty2PageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<Empty2Page>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            var page = GetPage<Empty2Page>();
            Assert.IsNotNull(page);
        }
    }
}
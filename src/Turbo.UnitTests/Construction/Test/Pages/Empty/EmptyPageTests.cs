using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Empty;

namespace Turbo.UnitTests.Construction.Test.Pages.Empty
{
    [TestClass]
    public class EmptyPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<EmptyPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            var page = GetPage<EmptyPage>();
            Assert.IsNotNull(page);
        }
    }
}
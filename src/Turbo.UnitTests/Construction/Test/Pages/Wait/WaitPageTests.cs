using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Wait;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.Wait
{
    [TestClass]
    public class WaitPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<WaitPage>(app);
        }

        [TestMethod]
        public void Waits_for_element_on_the_page_before_page_loads()
        {
            WebDriver
                .Expect("#wait-for", new NullElement())
                .ReturnAfter(5.Attempt());

            var page = GetPage<WaitPage>();

            Assert.IsNotNull(page);

            var expectation = WebDriver.Verify("#wait-for");
            Assert.IsTrue(expectation.WasReturned(), $"Attempts: {expectation.Requests}");
        }

        [TestMethod]
        public void Waits_for_element_on_part()
        {
            WebDriver
                .Expect("#wait-for", new NullElement())
                .ReturnAfter(5.Attempt());

            var part = new NullElement();
            WebDriver.Expect("#waitable", part);

            part.Expect(".cool", new NullElement());
            part.Expect("#something", new NullElement())
                .ReturnAfter(3.Attempt());

            var page = GetPage<WaitPage>();

            Assert.IsNotNull(page);
            Assert.IsNotNull(page.Waitable);

            var expectation = part.Verify("#something");
            Assert.IsTrue(expectation.WasReturned(), $"Attemps: {expectation.Requests}.");
        }
    }
}
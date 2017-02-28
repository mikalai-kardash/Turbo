using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.Construction.Test.Pages.HasPart;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.Normal;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.Normal
{
    [TestClass]
    public class NormalPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<NormalPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            var normalElement = new NullElement();
            WebDriver.Expect(".something", new NullElement());
            WebDriver.Expect("#id", normalElement);

            var simple = new NullElement();
            normalElement.Expect(".simplePart", simple);
            normalElement.Expect(".normal", new NullElement());

            simple.Expect(".byClass", new NullElement());
            simple.Expect("a", new NullElement(), new NullElement(), new NullElement());

            var page = GetPage<NormalPage>();

            Assert.IsNotNull(page.GetElement());

            var normal = page.Normal;

            Assert.IsNotNull(normal, "normal");
            Assert.IsNotNull(normal.GetElement());

            HasPartPageTests.VerifySimple(normal.Simple);
        }
    }
}
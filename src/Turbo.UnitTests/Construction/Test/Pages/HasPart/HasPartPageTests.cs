using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Test;
using Turbo.UnitTests.PageObjects.Test.Pages.HasPart;
using Turbo.UnitTests.PageObjects.Test.Parts.Simple;
using Turbo.UnitTests.Stubs.WebDriver.Null;

namespace Turbo.UnitTests.Construction.Test.Pages.HasPart
{
    [TestClass]
    public class HasPartPageTests : PageTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            var app = RegisterApp<TestApp>();
            RegisterPage<HasPartPage>(app);
        }

        [TestMethod]
        public void Creates_page()
        {
            var simple = new NullElement();
            WebDriver.Expect(".simplePart", simple);

            simple.Expect(".byClass", new NullElement());
            simple.Expect("a", new NullElement());

            var page = GetPage<HasPartPage>();
            VerifySimple(page.Simple);
        }

        public static void VerifySimple(Simple simple)
        {
            Assert.IsNotNull(simple, "Simple");

            // Field - WebDriver
            Assert.IsNotNull(simple.GetBrowser(), "Browser");

            // Field - Root
            Assert.IsNotNull(simple.GetRoot(), "root");
            Assert.IsNotNull(simple.Get_Root(), "_root");
            Assert.IsNotNull(simple.GetContainer(), "container");
            Assert.IsNotNull(simple.Get_Container(), "_container");

            // Field - Element
            Assert.IsNotNull(simple.GetElement(), "element");

            // Field - Element[]
            Assert.IsNotNull(simple.GetLinks(), "links");

            // Property - WebDriver
            Assert.IsNotNull(simple.Browser, "Browser (property)");

            // Property - Root
            Assert.IsNull(simple.Root, "Root");
            Assert.IsNull(simple._Root, "_Root");
            Assert.IsNotNull(simple.GetROot(), "rOot");

            // Property - Element
            Assert.IsNull(simple.Element, "Element");

            // Property - Element[]
            Assert.IsNull(simple.Links, "Links");
        }
    }
}
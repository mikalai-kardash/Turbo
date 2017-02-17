using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turbo.UnitTests
{
    [TestClass]
    public class SelectorFinderTests
    {
        [TestMethod]
        public void Finds_selector_by_field()
        {
            AssertSelector("element", "element");
            AssertSelector("_element", "element");
            AssertSelector("Element", "element");
            AssertSelector("element", "e-lement");
            AssertSelector("element", "elemen_t");
            AssertSelector("element", "elem ent");
            AssertSelector("element", "elEm ent");
        }

        private static void AssertSelector(string fieldName, string elementName)
        {
            var selector = $"{fieldName}+{elementName}";

            var finder = new SelectorFinder(new Dictionary<string, string>()
            {
                {elementName, selector}
            });

            Assert.AreEqual(selector, finder.GetSelector(fieldName));
        }
    }
}
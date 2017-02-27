using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Events.Message;

namespace Turbo.UnitTests.Events
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Show_should_show()
        {
            Assert.AreEqual("^11", "11".Show(-1));
            Assert.AreEqual("^11", "11".Show(0));
            Assert.AreEqual("1^1", "11".Show(1));
            Assert.AreEqual("11^", "11".Show(2));
            Assert.AreEqual("11^", "11".Show(3));
        }
    }
}
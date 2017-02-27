using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turbo.UnitTests.Events.Reports
{
    [TestClass]
    public class SimpleReportTests
    {
        private SampleReport _report;

        [TestInitialize]
        public void SetUp()
        {
            _report = new SampleReport();
        }


        [TestMethod]
        public void Creates_consumer()
        {
            var consumer = _report.CreateConsumer("test");
            Assert.IsNotNull(consumer);
        }

        [TestMethod]
        public void Creates_one_instance_per_source()
        {
            var first = _report.CreateConsumer("first");
            var same = _report.CreateConsumer("first");
            var second = _report.CreateConsumer("second");

            Assert.AreSame(first, same);
            Assert.AreNotSame(first, second);
        }
    }
}
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Events;
using Turbo.Events.Model;
using Turbo.UnitTests.Events.Reports.Test;

namespace Turbo.UnitTests.Events
{
    [TestClass]
    public class EventSystemTests
    {
        private TestReport _report;
        private EventSink<EventSystemTests> _sink;

        private static class EventIds
        {
            public const int Test = 1;
        }

        [TestInitialize]
        public void SetUp()
        {
            var es = new EventSystem();
            _report = new TestReport();
            es.AddReport(_report);

            _sink = new EventSink<EventSystemTests>(es);
        }

        [TestMethod]
        public void Send_information_message()
        {
            const string message = "Test message";
            
            _sink.Info(EventIds.Test, message);

            var e = _report.Events.FirstOrDefault();
            Assert.IsNotNull(e);
            Assert.AreEqual(EventIds.Test, e.PartialId);
            Assert.AreEqual(message, e.Message.ToString());
            Assert.AreEqual(typeof(EventSystemTests).FullName, e.Source);
        }

        [TestMethod]
        public void Send_templated_message()
        {
            const string template = "template {count}";

            _sink.Info(EventIds.Test, template, 3);

            var e = _report.Events.FirstOrDefault();
            Assert.IsNotNull(e);
            Assert.AreEqual(template, e.Message.Template);
            Assert.AreEqual("template 3", e.Message.ToString());
        }

        [TestMethod]
        public void Send_scoped_events()
        {
            const string message = "test";

            using (_sink.Measure(EventIds.Test, message))
            {
                var a = 33;
            }

            Assert.AreEqual(2, _report.Events.Count);

            var first = _report.Events.OfType<ScopedEvent>().First();
            var second = _report.Events.OfType<ScopedEvent>().Last();

            Assert.AreEqual(message, first.Message.Template);
            Assert.IsNotNull(first.ScopeId);

            Assert.AreEqual(message, second.Message.Template);
            Assert.IsNotNull(second.ScopeId);

            Assert.AreEqual(first.ScopeId, second.ScopeId);
        }

        [TestMethod]
        public void Send_scope_events_once()
        {
            var scope = _sink.Measure(EventIds.Test, "test");
            scope.Dispose();
            scope.Dispose();

            Assert.AreEqual(2, _report.Events.Count);
        }
    }
}
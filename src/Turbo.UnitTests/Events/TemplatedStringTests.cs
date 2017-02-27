using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.Events.Message;

namespace Turbo.UnitTests.Events
{
    [TestClass]
    public class TemplatedStringTests
    {
        [TestMethod]
        public void Simple_string()
        {
            var ts = new TemplatedString("simple");

            Assert.AreEqual("simple", ts.ToString());
        }

        [TestMethod]
        public void Simple_temlate()
        {
            var count = 3;
            var ts = new TemplatedString("count: {count}", count);

            Assert.AreEqual("count: 3", ts.ToString());
        }

        [TestMethod]
        public void Several_variables()
        {
            var c = 1;
            var s = "two";
            var ts = new TemplatedString("vars {first}, {second}", c, s);

            Assert.AreEqual("vars 1, two", ts.ToString());
        }

        [TestMethod]
        public void Repeat_variables()
        {
            var c = 1;
            var ts = new TemplatedString("repeat {c}{c} {c}", c);

            Assert.AreEqual("repeat 11 1", ts.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Throw_when_two_opening_brakets()
        {
            new TemplatedString("{{").ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Throw_when_two_closing_brakets()
        {
            new TemplatedString("}}").ToString();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Throw_when_variable_has_no_name()
        {
            new TemplatedString("{}").ToString();
        }
    }
}
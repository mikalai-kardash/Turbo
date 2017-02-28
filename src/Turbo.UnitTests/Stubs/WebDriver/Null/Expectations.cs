using System.Collections.Generic;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class Expectations
    {
        private readonly IDictionary<By, Expectation> _expectations
            = new Dictionary<By, Expectation>();

        public Expectation Add(By by, NullElement[] elements)
        {
            var expectation = new Expectation
            {
                Selector = by,
                Elements = elements
            };

            _expectations.Add(by, expectation);

            return expectation;
        }

        public bool TryGet(By by, out NullElement[] elements)
        {
            elements = null;
            Expectation expectation;
            var exists = _expectations.TryGetValue(by, out expectation);
            if (exists)
            {
                expectation.Requests++;
                if (expectation.Requests >= expectation.Return)
                {
                    elements = expectation.Elements;
                    return true;
                }
            }

            return false;
        }

        public bool TryGetExpectation(By by, out Expectation expectation)
        {
            return _expectations.TryGetValue(by, out expectation);
        }
    }
}
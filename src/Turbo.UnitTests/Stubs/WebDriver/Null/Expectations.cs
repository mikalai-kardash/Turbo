using System.Collections.Generic;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class Expectations
    {
        private readonly IDictionary<By, NullElement[]> _expectations
            = new Dictionary<By, NullElement[]>();

        public void Add(By by, NullElement[] elements)
        {
            _expectations.Add(by, elements);
        }

        public bool TryGet(By by, out NullElement[] elements)
        {
            return _expectations.TryGetValue(by, out elements);
        }
    }
}
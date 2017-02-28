using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class Expectation
    {
        public By Selector { get; set; }
        public NullElement[] Elements { get; set; }
        public int Return { get; set; } = 0;
        public int Requests { get; set; } = 0;

        public Expectation ReturnAfter(int attempt)
        {
            Return = attempt;
            return this;
        }

        public bool WasReturned()
        {
            return Requests >= Return;
        }
    }
}
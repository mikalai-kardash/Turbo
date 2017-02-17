using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullOptions : IOptions
    {
        public ITimeouts Timeouts()
        {
            throw new System.NotImplementedException();
        }

        public ICookieJar Cookies { get; }
        public IWindow Window { get; }
        public ILogs Logs { get; }
    }
}
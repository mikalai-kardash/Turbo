using System;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullNavigation : INavigation
    {
        public void Back()
        {
        }

        public void Forward()
        {
        }

        public void GoToUrl(string url)
        {
        }

        public void GoToUrl(Uri url)
        {
        }

        public void Refresh()
        {
        }
    }
}
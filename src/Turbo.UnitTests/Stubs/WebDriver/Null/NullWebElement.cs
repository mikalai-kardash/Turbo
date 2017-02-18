using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullWebElement : IWebElement
    {
        private ReadOnlyCollection<IWebElement> _findElementsExpectation;

        public IWebElement FindElement(By by)
        {
            return new NullWebElement
            {
                CssSelector = by.ToString()
            };
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            if (_findElementsExpectation != null)
            {
                return _findElementsExpectation;
            }
            return new List<IWebElement>().AsReadOnly();
        }

        public void Clear()
        {
        }

        public void SendKeys(string text)
        {
        }

        public void Submit()
        {
        }

        public void Click()
        {
        }

        public string GetAttribute(string attributeName)
        {
            throw new System.NotImplementedException();
        }

        public string GetCssValue(string propertyName)
        {
            throw new System.NotImplementedException();
        }

        public string TagName { get; }

        public string Text { get; }

        public bool Enabled { get; }

        public bool Selected { get; }

        public Point Location { get; }

        public Size Size { get; }

        public bool Displayed { get; }

        public string CssSelector { get; set; }

        public void ExpectFindElements(IEnumerable<IWebElement> returns)
        {
            _findElementsExpectation = new List<IWebElement>(returns).AsReadOnly();
        }
    }
}
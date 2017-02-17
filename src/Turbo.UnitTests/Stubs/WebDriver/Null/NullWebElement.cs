using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullWebElement : IWebElement
    {
        public IWebElement FindElement(By by)
        {
            return new NullWebElement();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
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
    }
}
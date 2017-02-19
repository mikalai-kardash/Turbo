using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;

namespace Turbo.UnitTests.Stubs.WebDriver.Null
{
    public class NullElement : IWebElement
    {
        private ReadOnlyCollection<NullElement> _findElementsExpectation;

        public IWebElement FindElement(By by)
        {
            return new NullElement
            {
                FoundBy = by,
                Parent = this
            };
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            if (_findElementsExpectation == null)
            {
                return new List<IWebElement>().AsReadOnly();
            }

            foreach (var element in _findElementsExpectation)
            {
                element.FoundBy = by;
                element.Parent = this;
            }

            return _findElementsExpectation
                .Select(e => e as IWebElement)
                .ToList()
                .AsReadOnly();
        }

        public IWebElement Parent { get; set; }

        public override string ToString()
        {
            return $"{Parent} >> {FoundBy}";
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

        public By FoundBy { get; set; }

        public void ExpectFindElements(IEnumerable<NullElement> returns)
        {
            _findElementsExpectation = new List<NullElement>(returns).AsReadOnly();
        }
    }
}
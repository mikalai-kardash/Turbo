using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using Turbo.Events;

namespace Turbo.Statistics
{
    public class WebElementWrapper : IWebElement
    {
        private readonly IWebElement _element;
        private readonly EventSink<WebElementWrapper> _sink;

        public WebElementWrapper(IWebElement element, EventSink<WebElementWrapper> sink)
        {
            _element = element;
            _sink = sink;
        }

        public IWebElement FindElement(By by)
        {
            _sink.Info(EventIds.SelectorUsage, "Find element by {by}.", by);
            return _element.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _element.FindElements(by);
        }

        public void Clear()
        {
            _element.Clear();
        }

        public void SendKeys(string text)
        {
            _element.SendKeys(text);
        }

        public void Submit()
        {
            _element.Submit();
        }

        public void Click()
        {
            _element.Click();
        }

        public string GetAttribute(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return _element.GetCssValue(propertyName);
        }

        public string TagName => _element.TagName;
        public string Text => _element.Text;
        public bool Enabled => _element.Enabled;
        public bool Selected => _element.Selected;
        public Point Location => _element.Location;
        public Size Size => _element.Size;
        public bool Displayed => _element.Displayed;
    }
}
using OpenQA.Selenium;

namespace Turbo.UnitTests.PageObjects.Test.Parts.Simple
{
    public class Simple
    {
        #region Field - IWebDriver

        private IWebDriver browser;

        public IWebDriver GetBrowser()
        {
            return browser;
        }

        #endregion

        #region Field - Root

        private IWebElement root;

        private IWebElement _root;

        private IWebElement container;

        private IWebElement _container;

        public IWebElement GetRoot()
        {
            return root;
        }

        public IWebElement Get_Root()
        {
            return _root;
        }

        public IWebElement GetContainer()
        {
            return container;
        }

        public IWebElement Get_Container()
        {
            return _container;
        }

        #endregion

        #region Field - Element

        private IWebElement element;

        public IWebElement GetElement()
        {
            return element;
        }

        #endregion

        #region Field - Element[]

        private IWebElement[] links;

        public IWebElement[] GetLinks()
        {
            return links;
        }

        #endregion

        #region Property - WebDriver

        public IWebDriver Browser { get; set; }

        #endregion

        #region Property - Root

        public IWebElement Root { get; set; }
        public IWebElement _Root { get; set; }
        private IWebElement rOot { get; set; }

        public IWebElement GetROot()
        {
            return rOot;
        }

        #endregion

        #region Property - Element

        public IWebElement Element { get; set; }

        #endregion

        #region Element[]

        public IWebElement Links { get; set; }

        #endregion
    }
}
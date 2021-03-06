﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Turbo.Construction.Steps.WaitForElement
{
    public class WaitForElement : IWaitForElement
    {
        private readonly string _cssSelector;
        private readonly TimeSpan _60Seconds = TimeSpan.FromSeconds(60);

        public WaitForElement(string cssSelector)
        {
            _cssSelector = cssSelector;
        }

        public void Run(IWebDriver driver)
        {
            var by = By.CssSelector(_cssSelector);
            var wait = new WebDriverWait(driver, _60Seconds);

            wait.Until(d => d.FindElement(by) != null);
        }

        public void Run(IWebDriver driver, IWebElement element)
        {
            var by = By.CssSelector(_cssSelector);
            var wait = new WebDriverWait(driver, _60Seconds);

            wait.Until(d => element.FindElement(by) != null);
        }
    }
}
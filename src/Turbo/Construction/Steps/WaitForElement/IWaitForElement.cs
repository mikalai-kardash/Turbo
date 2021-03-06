﻿using OpenQA.Selenium;

namespace Turbo.Construction.Steps.WaitForElement
{
    public interface IWaitForElement
    {
        void Run(IWebDriver driver);
        void Run(IWebDriver driver, IWebElement element);
    }
}
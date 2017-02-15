using Automation.PageObjects.CustomerPortal;
using Automation.PageObjects.CustomerPortal.Pages.LogIn;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Turbo;

namespace Automation.CustomerPortal.Features.LogIn
{
    [Binding]
    public class LogInSteps
    {
        private readonly IWebDriver _browser;
        private readonly TurboFactory _turbo;
        private LogInPage _logInPage;

        public LogInSteps(IWebDriver browser, TurboFactory turbo)
        {
            _browser = browser;
            _turbo = turbo;
        }

        [Given(@"I am on the Log In page")]
        public void GivenIAmOnTheLogInPage()
        {
            _logInPage = _turbo.GetPage<CustomerPortalApp, LogInPage>(_browser);
        }
        
        [Given(@"I have entered superuser@costar\.com as user name")]
        public void GivenIHaveEnteredSuperuserCostar_ComAsUserName()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have entered superuser as password")]
        public void GivenIHaveEnteredSuperuserAsPassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I press Login button")]
        public void WhenIPressLoginButton()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

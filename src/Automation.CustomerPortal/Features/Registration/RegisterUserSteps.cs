using TechTalk.SpecFlow;

namespace Automation.CustomerPortal.Features.Registration
{
    [Binding]
    public class RegisterUserSteps : Steps
    {
        [Given(@"I have entered '(.*)' into First Name")]
        public void GivenIHaveEnteredIntoFirstName(string firstName)
        {
            this.ScenarioContext.Pending();
        }

        [Given(@"I have entered '(.*)' into Last Name")]
        public void GivenIHaveEnteredIntoLastName(string lastName)
        {
            this.ScenarioContext.Pending();
        }

        [Given(@"I have entered '(.*)' into Email")]
        public void GivenIHaveEnteredIntoEmail(string email)
        {
            this.ScenarioContext.Pending();
        }

        [Given(@"I have entered '(.*)' into Password")]
        public void GivenIHaveEnteredIntoPassword(string password)
        {
            this.ScenarioContext.Pending();
        }

        [When(@"I press Submit")]
        public void WhenIPressSubmit()
        {
            this.ScenarioContext.Pending();
        }

        [Then(@"I must be redirected to Property Access Request page")]
        public void ThenIMustBeRedirectedToPropertyAccessRequestPage()
        {
            this.ScenarioContext.Pending();
        }
    }
}
using Automation.PageObjects.CustomerPortal.Pages.LogIn.Parts.LogIn;
using Automation.PageObjects.CustomerPortal.Pages.LogIn.Parts.Register;
using Automation.PageObjects.CustomerPortal.Parts.Footer;
using Automation.PageObjects.CustomerPortal.Parts.Header;

namespace Automation.PageObjects.CustomerPortal.Pages.LogIn
{
    public class LogInPage
    {
        public Header Header { get; set; }
        public CustomerLogIn LogIn { get; set; }
        public RegisterCustomer Register { get; set; }
        public Footer Footer { get; set; }
    }
}
using Automation.PageObjects.CustomerPortal;
using Automation.PageObjects.CustomerPortal.Pages.AdAnalytics;
using Automation.PageObjects.CustomerPortal.Pages.Dashboard;
using Automation.PageObjects.CustomerPortal.Pages.LogIn;
using Automation.PageObjects.CustomerPortal.Pages.MyListings;
using Automation.PageObjects.CustomerPortal.Pages.Reviews;
using BoDi;
using TechTalk.SpecFlow;
using Turbo;

namespace Automation.CustomerPortal
{
    [Binding]
    public class TurboInit : SpecFlow.Bindings.Turbo
    {
        public TurboInit(IObjectContainer container) : base(container)
        {
        }

        [BeforeTestRun]
        public void ConfigureApp()
        {
            TurboInitializer.AddApp<CustomerPortalApp>();
            TurboInitializer.AddPage<CustomerPortalApp, LogInPage>();
            TurboInitializer.AddPage<CustomerPortalApp, AdAnalyticsPage>();
            TurboInitializer.AddPage<CustomerPortalApp, DashboardPage>();
            TurboInitializer.AddPage<CustomerPortalApp, MyListingsPage>();
            TurboInitializer.AddPage<CustomerPortalApp, ReviewsDashboardPage>();
        }
    }
}
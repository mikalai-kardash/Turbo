using OpenQA.Selenium;

namespace Turbo
{
    public class TurboFactory
    {
        private readonly TurboConfiguration _configuration;

        public TurboFactory(TurboConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TPage GetPage<TApp, TPage>(IWebDriver driver)
        {
            var app = typeof(TApp);
            var page = typeof(TPage);



            return default(TPage);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using OpenQA.Selenium;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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
            var appMetadata = GetMetadata<ApplicationMetadata, TApp>();
            var pageMetadata = GetMetadata<PageMetadata, TPage>();

            var page = _configuration.ObjectFactory.GetInstance<TPage>();

            return default(TPage);
        }

        private static TMeta GetMetadata<TMeta, TApp>()
        {
            var metadata = default(TMeta);

            var app = typeof(TApp);
            var assembly = app.Assembly;
            var resource = $"{app.FullName}.yaml";

            var resources = new HashSet<string>(assembly.GetManifestResourceNames());

            if (resources.Contains(resource))
            {
                var stream = assembly.GetManifestResourceStream(resource);
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var deserializer = new DeserializerBuilder()
                            .WithNamingConvention(new PascalCaseNamingConvention())
                            .Build();

                        metadata = deserializer.Deserialize<TMeta>(reader);
                    }
                }
            }

            return metadata;
        }
    }
}
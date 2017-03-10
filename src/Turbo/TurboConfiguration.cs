using System.Reflection;
using Turbo.DI;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;

namespace Turbo
{
    public class TurboConfiguration
    {
        public IObjectFactory ObjectFactory { get; set; }
        public Assembly[] PageObjects { get; set; }
        public IMetadataLoader MetadataLoader { get; set; }

        public static TurboConfiguration Default => new TurboConfiguration
        {
            PageObjects = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.GetCallingAssembly(),
                Assembly.GetEntryAssembly()
            },
            ObjectFactory = new SimpleObjectFactory(),
            MetadataLoader = new YamlMetadataLoader()
        };
    }
}
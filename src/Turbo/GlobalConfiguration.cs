using Turbo.DI;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;

namespace Turbo
{
    internal static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
            MetadataLoader = new YamlMetadataLoader();
            ObjectFactory = new DefaultObjectFactory();
        }

        public static IMetadataLoader MetadataLoader { get; }

        public static DefaultObjectFactory ObjectFactory { get; }
    }
}
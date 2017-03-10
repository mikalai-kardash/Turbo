using Turbo.DI;
using Turbo.Metadata.Yaml;

namespace Turbo.Metadata
{
    public class MetadataModule : Module
    {
        public MetadataModule()
        {
            Registry.AddType<IMetadataLoader, YamlMetadataLoader>();
        }
    }
}
using System.Collections.Generic;
using System.IO;
using Turbo.Metadata;
using Turbo.Metadata.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Turbo
{
    // todo: move to yaml specific assembly
    public class YamlMetadataLoader : IMetadataLoader
    {
        public AppMeta<T> GetAppMeta<T>()
        {
            return GetMetadata<App, T>().ToMeta<T>();
        }

        public PageMeta<T> GetPageMeta<T>()
        {
            return GetMetadata<Page, T>().ToMeta<T>();
        }

        public PartMeta<T> GetPartMeta<T>()
        {
            return GetMetadata<Part, T>().ToMeta<T>();
        }

        private static TMeta GetMetadata<TMeta, T>()
        {
            var metadata = default(TMeta);

            var type = typeof(T);
            var assembly = type.Assembly;
            var resource = $"{type.FullName}.yaml";

            var resources = new HashSet<string>(assembly.GetManifestResourceNames());

            if (resources.Contains(resource))
            {
                var stream = assembly.GetManifestResourceStream(resource);
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var deserializer = new DeserializerBuilder()
                            .WithNamingConvention(new CamelCaseNamingConvention())
                            .Build();

                        metadata = deserializer.Deserialize<TMeta>(reader);
                    }
                }
            }

            return metadata;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Turbo.Metadata.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Turbo.Metadata.Yaml
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

        public Metadata<Part> GetPartMeta(Type partType)
        {
            return GetMetadata<Part>(partType).ToMeta(partType);
        }

        public Metadata<Page> GetPageMeta(Type pageType)
        {
            return GetMetadata<Page>(pageType).ToMeta(pageType);
        }

        private static TMeta GetMetadata<TMeta, T>()
        {
            return GetMetadata<TMeta>(typeof(T));
        }

        private static TMeta GetMetadata<TMeta>(Type type)
        {
            var metadata = default(TMeta);

            var assembly = type.Assembly;
            var resource = $"{type.FullName}.yaml";
            var resources = new HashSet<string>(assembly.GetManifestResourceNames());

            if (!resources.Contains(resource))
            {
                return metadata;
            }


            var stream = assembly.GetManifestResourceStream(resource);
            if (stream == null)
            {
                return metadata;
            }

            using (var reader = new StreamReader(stream))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();

                metadata = deserializer.Deserialize<TMeta>(reader);
            }

            return metadata;
        }
    }
}
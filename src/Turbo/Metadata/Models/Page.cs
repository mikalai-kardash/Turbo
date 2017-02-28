using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Turbo.Metadata.Models
{
    public class Page
    {
        public string Url { get; set; }
        public string Wait { get; set; }

        public IDictionary<string, string> Elements { get; set; }
    }
}
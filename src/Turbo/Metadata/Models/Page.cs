using System.Collections.Generic;

namespace Turbo.Metadata.Models
{
    public class Page
    {
        public string Url { get; set; }

        public IDictionary<string, string> Elements { get; set; }
    }
}
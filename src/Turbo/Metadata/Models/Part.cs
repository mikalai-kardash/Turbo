using System.Collections.Generic;

namespace Turbo.Metadata.Models
{
    public class Part
    {
        public string Selector { get; set; }

        public Dictionary<string, string> Elements { get; set; }
    }
}
using System.Collections.Generic;

namespace Turbo.Metadata.Models
{
    public class Part
    {
        public string Selector { get; set; }

        public bool Immediate { get; set; } = true;

        public Dictionary<string, string> Elements { get; set; }
    }
}
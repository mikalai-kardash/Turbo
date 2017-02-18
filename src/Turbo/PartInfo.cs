using Turbo.Construction;
using Turbo.Metadata;
using Turbo.Metadata.Models;

namespace Turbo
{
    public class PartInfo
    {
        public PartInfo()
        {
            Analysis = new Analysis();
        }

        public Analysis Analysis { get; }

        public Metadata<Part> Part { get; set; }
        public ISelectorFinder Finder { get; set; }
    }
}
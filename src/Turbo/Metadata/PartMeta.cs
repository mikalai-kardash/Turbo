using Turbo.Metadata.Models;

namespace Turbo.Metadata
{
    public class PartMeta<T> : Metadata<T, Part>
    {
        public PartMeta(Part meta) : base(meta)
        {
        }
    }
}
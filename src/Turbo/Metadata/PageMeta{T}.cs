using Turbo.Metadata.Models;

namespace Turbo.Metadata
{
    public class PageMeta<T> : Metadata<T, Page>
    {
        public PageMeta(Page meta) : base(meta)
        {
        }
    }
}
using Turbo.Metadata.Models;

namespace Turbo.Metadata
{
    public class AppMeta<T> : Metadata<T, App>
    {
        public AppMeta(App meta) : base(meta)
        {
        }
    }
}
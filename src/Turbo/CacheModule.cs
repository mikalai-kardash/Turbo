using Turbo.Cache;
using Turbo.DI;

namespace Turbo
{
    public class CacheModule : Module
    {
        public CacheModule()
        {
            Registry
                .AddType<IInfoProvider, InfoProvider>();
        }
    }
}
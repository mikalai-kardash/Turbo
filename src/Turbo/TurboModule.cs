using Turbo.DI;

namespace Turbo
{
    public class TurboModule : Module
    {
        public TurboModule()
        {
            Registry.AddType<ITurboFactory, TurboFactory>();
        }
    }
}
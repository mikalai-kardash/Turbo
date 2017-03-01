using Turbo.DI;

namespace Turbo.UnitTests.DI.Modules.Simple
{
    internal class SimpleModule : Module
    {
        public SimpleModule()
        {
            Registry.AddType<IService, Service>();
        }
    }
}
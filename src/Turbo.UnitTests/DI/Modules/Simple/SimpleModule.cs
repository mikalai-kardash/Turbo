using Turbo.DI;

namespace Turbo.UnitTests.DI.Modules.Simple
{
    internal class SimpleModule : Module
    {
        public SimpleModule()
        {
            Registry.Instance(new Service());

            Registry.AddType<IService, Service>();
        }
    }
}
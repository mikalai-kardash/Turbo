using Turbo.DI;

namespace Turbo.UnitTests.DI.Modules.Global
{
    internal class GlobalModule : Module
    {
        public GlobalModule()
        {
            Registry.Instance(new GlobalService());

            Registry.AddType<IGlobalService, GlobalService>();
        }
    }
}
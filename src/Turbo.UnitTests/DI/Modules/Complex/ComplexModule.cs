using Turbo.DI;
using Turbo.UnitTests.DI.Modules.Simple;

namespace Turbo.UnitTests.DI.Modules.Complex
{
    internal class ComplexModule : Module
    {
        public ComplexModule()
        {
            Uses<SimpleModule>();

            Registry.AddType<IOtherService, OtherService>();
        }
    }
}
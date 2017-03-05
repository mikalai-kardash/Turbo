using Turbo.DI;
using Turbo.UnitTests.DI.Modules.Simple;

namespace Turbo.UnitTests.DI.Modules.Complex
{
    internal class ComplexModule : Module
    {
        public ComplexModule()
        {
            Include<SimpleModule>();

            Registry.Instance(new OtherService());

            Registry.AddType<IOtherService, OtherService>();
        }
    }
}
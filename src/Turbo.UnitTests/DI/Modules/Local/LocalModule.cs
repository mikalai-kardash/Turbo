using Turbo.DI;
using Turbo.UnitTests.DI.Modules.Complex;

namespace Turbo.UnitTests.DI.Modules.Local
{
    internal class LocalModule : Module
    {
        public LocalModule()
        {
            Include<ComplexModule>();

            Registry.Instance(new LocalService());

            Registry.AddType<ILocalService, LocalService>();
        }
    }
}
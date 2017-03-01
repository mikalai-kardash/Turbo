using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.DI;
using Turbo.UnitTests.DI.Modules.Complex;
using Turbo.UnitTests.DI.Modules.Simple;

namespace Turbo.UnitTests.DI.Modules
{
    [TestClass]
    public class ComplexModuleTests
    {
        [TestMethod]
        public void Creates_objects()
        {
            var module = new ComplexModule();

            var service = module.GetInstance<IService>();
            var other = module.GetInstance<IOtherService>();

            Assert.IsNotNull(service);
            Assert.IsNotNull(other);
        }
    }
}
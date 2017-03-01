using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.DI;
using Turbo.UnitTests.DI.Modules.Simple;

namespace Turbo.UnitTests.DI.Modules
{
    [TestClass]
    public class SimpleModuleTests
    {
        [TestMethod]
        public void Can_create_types()
        {
            var module = new SimpleModule();

            Assert.IsNotNull(module.GetInstance<IService>());
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.DI;
using Turbo.UnitTests.DI.Modules.Complex;
using Turbo.UnitTests.DI.Modules.Global;
using Turbo.UnitTests.DI.Modules.Local;
using Turbo.UnitTests.DI.Modules.Simple;

namespace Turbo.UnitTests.DI
{
    [TestClass]
    public class TwoTieredFactoryTests
    {
        private IObjectFactory _factory;

        [TestInitialize]
        public void SetUp()
        {
            _factory = new TwoTieredFactory(
                new GlobalModule(), 
                new LocalModule());
        }

        [TestMethod]
        public void Creates_service()
        {
            CanCreate<IGlobalService>();
            CanCreate<IService>();
            CanCreate<IOtherService>();
            CanCreate<ILocalService>();
        }

        [TestMethod]
        public void Gets_service()
        {
            CanGet<GlobalService>();
            CanGet<Service>();
            CanGet<OtherService>();
            CanGet<LocalService>();
        }

        private void CanGet<T>()
        {
            var first = _factory.GetInstance<T>();
            var second = _factory.GetInstance<T>();

            var typeName = typeof(T).Name;

            Assert.IsNotNull(first, typeName);
            Assert.AreSame(first, second, typeName);
        }

        private T CanCreate<T>()
        {
            var instance = _factory.GetInstance<T>();
            Assert.IsNotNull(instance, typeof(T).Name);
            return instance;
        }
    }
}
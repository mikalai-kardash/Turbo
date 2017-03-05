using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.DI;
using Turbo.Metadata;
using Turbo.Metadata.Yaml;
using Turbo.UnitTests.DI.Stubs.Complex;
using Turbo.UnitTests.DI.Stubs.Simple;
using Turbo.UnitTests.DI.Stubs.Typed;
using Turbo.UnitTests.DI.Stubs.Unregistered;

namespace Turbo.UnitTests.DI
{
    [TestClass]
    public class DefaultObjectFactoryTests
    {
        private DefaultObjectFactory _factory;
        private IObjectRegistry _registry;

        [TestInitialize]
        public void SetUp()
        {
            _factory = new DefaultObjectFactory();
            _registry = _factory;
        }

        [TestMethod]
        public void Creates_unregistered_class()
        {
            Assert.IsNotNull(_factory.GetInstance<SimpleClass>());
        }

        [TestMethod]
        public void Creates_itself()
        {
            var createdInterface = _factory.GetInstance<IObjectFactory>();
            var createdClass = _factory.GetInstance<DefaultObjectFactory>();

            Assert.IsNotNull(createdInterface);
            Assert.IsNotNull(createdClass);

            Assert.AreSame(_factory, createdInterface);
            Assert.AreSame(createdInterface, createdClass);
        }

        [TestMethod]
        public void Creates_registered_instance()
        {
            var metadata = new YamlMetadataLoader();

            _factory.Instance<IMetadataLoader>(metadata);
            var created = _factory.GetInstance<IMetadataLoader>();

            Assert.IsNotNull(created);
            Assert.AreSame(metadata, created);
        }

        [TestMethod]
        public void Creates_registerd_type_without_dependencies()
        {
            _factory.AddType<ISimpleService, SimpleService>();
            var service = _factory.GetInstance<ISimpleService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Creates_registered_type_dependencies()
        {
            _factory.AddType<ISimpleService, SimpleService>();

            _factory
                .AddType<IComplexService, ComplexService>()
                .DependsOn<ISimpleService>();

            var service = _factory.GetInstance<IComplexService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Creates_registered_typed_service()
        {
            _registry.AddType(typeof(ITypedService<>), typeof(TypedService<>));
            var service = _factory.GetInstance<ITypedService<SomeClass>>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Creates_cached_instances()
        {
            _registry.AddType(typeof(ISimpleService), typeof(SimpleService)).Cached();

            var first = _factory.GetInstance(typeof(ISimpleService));
            var second = _factory.GetInstance(typeof(ISimpleService));

            Assert.AreEqual(first, second);
        }

        [TestMethod]
        public void Creates_named_instances()
        {
            const string name = "test";

            _registry.RegisterType(typeof(ISimpleService), typeof(SimpleService), name);
            var o = _factory.GetInstance(typeof(ISimpleService), name);

            Assert.IsNotNull(o);
        }
    }
}
using Turbo.Cache;
using Turbo.Construction;
using Turbo.DI;
using Turbo.Metadata;

namespace Turbo
{
    public static class TurboInitializer
    {
        public static ITurboFactory Init()
        {
            return Init(TurboConfiguration.Default);
        }

        public static ITurboFactory Init(TurboConfiguration configuration)
        {
            ConfigureObjectRegistry(
                (IObjectRegistry) configuration.ObjectFactory,
                configuration);

            return configuration.ObjectFactory.GetInstance<ITurboFactory>();
        }

        internal static void ConfigureObjectRegistry(IObjectRegistry registry, TurboConfiguration config)
        {
            registry.RegisterInstance(config);
            registry.RegisterInstance(config.MetadataLoader);

            RegisterBuiltInTypes(registry);
        }

        internal static void RegisterBuiltInTypes(IObjectRegistry registry)
        {
            registry
                .RegisterType<IInfoProvider, InfoProvider>()
                .DependsOn<IMetadataLoader>();

            registry
                .RegisterType<IPageAnalyzer, PageAnalyzer>()
                .DependsOn<IObjectFactory>()
                .DependsOn<IInfoProvider>();

            registry
                .RegisterType<IPartAnalyzer, PartAnalyzer>()
                .DependsOn<IObjectFactory>()
                .DependsOn<IInfoProvider>();

            registry
                .RegisterType<IPageFactory, PageFactory>()
                .DependsOn<IObjectFactory>();

            registry
                .RegisterType<ITurboFactory, TurboFactory>()
                .DependsOn<IObjectFactory>()
                .DependsOn<IInfoProvider>();
        }
    }
}
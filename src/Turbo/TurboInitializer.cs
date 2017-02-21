using System.Reflection;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.Construction;
using Turbo.DI;
using Turbo.Metadata;

namespace Turbo
{
    public static class TurboInitializer
    {
        static TurboInitializer()
        {
            ConfigureObjectRegistry(GlobalConfiguration.ObjectFactory, null);
        }

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
            //registry.RegisterInstance(config);
            registry.RegisterInstance(GlobalConfiguration.MetadataLoader);

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

        public static void AddApp<TApp>()
        {
            AddAppInternal<TApp>();
        }

        private static AppInfo AddAppInternal<TApp>()
        {
            var objectFactory = GlobalConfiguration.ObjectFactory;
            var loader = objectFactory.GetInstance<IMetadataLoader>();

            var meta = loader.GetAppMeta<TApp>();
            if (meta.Meta == null)
            {
                return null;
            }

            return TurboSync.AddApp(meta);
        }

        public static void AddPage<TApp, TPage>()
        {
            AppInfo appInfo;
            if (!TurboSync.TryGetApp(typeof(TApp), out appInfo))
            {
                appInfo = AddAppInternal<TApp>();
            }

            IObjectFactory objectFactory = GlobalConfiguration.ObjectFactory;
            var loader = objectFactory.GetInstance<IMetadataLoader>();

            var page = loader.GetPageMeta<TPage>();
            if (page.Meta == null)
            {
                return;
            }

            TurboSync.AddPage(appInfo.App, page);

            var pageAnalyzer = objectFactory.GetInstance<IPageAnalyzer>();
            pageAnalyzer.Analyze(typeof(TPage));
        }

        public static void Scan(Assembly assembly)
        {
        }
    }
}
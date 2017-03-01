using System;
using System.Collections.Generic;
using System.Linq;
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
            registry.Instance(GlobalConfiguration.MetadataLoader);

            RegisterBuiltInTypes(registry);
        }

        internal static void RegisterBuiltInTypes(IObjectRegistry registry)
        {
            registry
                .AddType<IInfoProvider, InfoProvider>()
                .DependsOn<IMetadataLoader>();

            registry
                .AddType<IPageAnalyzer, PageAnalyzer>()
                .DependsOn<IObjectFactory>()
                .DependsOn<IInfoProvider>();

            registry
                .AddType<IPartAnalyzer, PartAnalyzer>()
                .DependsOn<IObjectFactory>()
                .DependsOn<IInfoProvider>();

            registry
                .AddType<IPageFactory, PageFactory>()
                .DependsOn<IObjectFactory>();

            registry
                .AddType<ITurboFactory, TurboFactory>()
                .DependsOn<IObjectFactory>()
                .DependsOn<IInfoProvider>();
        }

        public static void AddApp<TApp>()
        {
            AddAppInternal(typeof(TApp));
        }

        private static AppInfo AddAppInternal(Type appType)
        {
            var objectFactory = GlobalConfiguration.ObjectFactory;
            var loader = objectFactory.GetInstance<IMetadataLoader>();

            var meta = loader.GetAppMeta(appType);
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
                appInfo = AddAppInternal(typeof(TApp));
            }

            AddPageInternal(appInfo, typeof(TPage));
        }

        private static void AddPageInternal(AppInfo appInfo, Type pageType)
        {
            IObjectFactory objectFactory = GlobalConfiguration.ObjectFactory;
            var loader = objectFactory.GetInstance<IMetadataLoader>();

            var page = loader.GetPageMeta(pageType);
            if (page.Meta == null)
            {
                return;
            }

            TurboSync.AddPage(appInfo.App, page);

            var pageAnalyzer = objectFactory.GetInstance<IPageAnalyzer>();
            pageAnalyzer.Analyze(pageType);
        }

        public static void Scan(Assembly assembly)
        {
            var types = GetTypes(assembly).ToList();

            var apps = types
                .Where(t => t.Name.EndsWith("App"))
                .Select(AddAppInternal)
                .ToList();

            var pages = types.Where(t => t.Name.EndsWith("Page"));
            foreach (var page in pages)
            {
                var pns = page.Namespace;
                if (pns == null)
                {
                    continue;
                } 

                foreach (var app in apps)
                {
                    var ans = app.App.Type.Namespace;
                    if (ans == null)
                    {
                        continue;
                    }

                    if (pns.StartsWith(ans, StringComparison.OrdinalIgnoreCase))
                    {
                        AddPageInternal(app, page);
                        break;
                    }
                }
            }
        }

        private static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null).ToArray();
            }
        }
    }
}
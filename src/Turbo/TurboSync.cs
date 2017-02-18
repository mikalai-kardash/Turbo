using System;
using System.Collections.Generic;
using Turbo.Construction;
using Turbo.Metadata;
using Turbo.Metadata.Models;

namespace Turbo
{
    internal static class TurboSync
    {
        private static readonly IDictionary<Type, AppInfo> _apps = new Dictionary<Type, AppInfo>();
        private static readonly IDictionary<Type, PageInfo> _pages = new Dictionary<Type, PageInfo>();
        private static readonly IDictionary<Type, PartInfo> _parts = new Dictionary<Type, PartInfo>();

        public static bool TryGetApp<TApp>(out AppInfo info)
        {
            return TryGetApp(typeof(TApp), out info);
        }

        public static bool TryGetApp(Type app, out AppInfo info)
        {
            return _apps.TryGetValue(app, out info);
        }

        public static bool TryGetPage<TPage>(out PageInfo info)
        {
            return TryGetPage(typeof(TPage), out info);
        }

        public static bool TryGetPage(Type page, out PageInfo info)
        {
            return _pages.TryGetValue(page, out info);
        }

        public static bool TryGetPart<TPart>(out PartInfo info)
        {
            return TryGetPart(typeof(TPart), out info);
        }

        public static bool TryGetPart(Type part, out PartInfo info)
        {
            return _parts.TryGetValue(part, out info);
        }

        public static AppInfo AddApp(Metadata<App> app)
        {
            var info = new AppInfo {App = app};
            _apps.Add(app.Type, info);
            return info;
        }

        public static PageInfo AddPage(Metadata<App> app, Metadata<Page> page)
        {
            var appInfo = _apps[app.Type];
            appInfo.AddPage(page);

            var elements = page.Meta?.Elements;
            var finder = GetSelectorFinder(elements);

            var pageInfo = new PageInfo
            {
                App = app,
                Page = page,
                Finder = finder
            };

            pageInfo.Analysis.Type = page.Type;

            _pages.Add(page.Type, pageInfo);

            return pageInfo;
        }

        private static ISelectorFinder GetSelectorFinder(IDictionary<string, string> elements)
        {
            var finder = elements == null
                ? new NullSelectorFinder() as ISelectorFinder
                : new SelectorFinder(elements);
            return finder;
        }

        public static PartInfo AddPart(Metadata<Part> part)
        {
            var elements = part.Meta?.Elements;
            var finder = GetSelectorFinder(elements);
            var partInfo = new PartInfo
            {
                Part = part,
                Finder = finder
            };

            partInfo.Analysis.RootCssSelector = part?.Meta?.Selector;
            partInfo.Analysis.Type = part.Type;

            _parts.Add(part.Type, partInfo);
            return partInfo;
        }
    }
}
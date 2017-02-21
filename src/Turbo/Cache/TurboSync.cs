using System;
using System.Collections.Generic;
using Turbo.Cache.Info;
using Turbo.Metadata;
using Turbo.Metadata.Models;

namespace Turbo.Cache
{
    // todo: hide this behind some fence
    internal static class TurboSync
    {
        private static readonly IDictionary<Type, AppInfo> Apps 
            = new Dictionary<Type, AppInfo>();

        private static readonly IDictionary<Type, PageInfo> Pages 
            = new Dictionary<Type, PageInfo>();

        private static readonly IDictionary<Type, PartInfo> Parts 
            = new Dictionary<Type, PartInfo>();

        #region TryGet

        public static bool TryGetApp<TApp>(out AppInfo info)
        {
            return TryGetApp(typeof(TApp), out info);
        }

        public static bool TryGetApp(Type app, out AppInfo info)
        {
            return Apps.TryGetValue(app, out info);
        }

        public static bool TryGetPage<TPage>(out PageInfo info)
        {
            return TryGetPage(typeof(TPage), out info);
        }

        public static bool TryGetPage(Type page, out PageInfo info)
        {
            return Pages.TryGetValue(page, out info);
        }

        public static bool TryGetPart<TPart>(out PartInfo info)
        {
            return TryGetPart(typeof(TPart), out info);
        }

        public static bool TryGetPart(Type part, out PartInfo info)
        {
            return Parts.TryGetValue(part, out info);
        }

        #endregion

        public static AppInfo AddApp(Metadata<App> app)
        {
            var info = new AppInfo {App = app};

            if (Apps.ContainsKey(app.Type))
            {
                Apps[app.Type] = info;
            }
            else
            {
                Apps.Add(app.Type, info);
            }

            return info;
        }

        public static PageInfo AddPage(Metadata<App> app, Metadata<Page> page)
        {
            var appInfo = Apps[app.Type];
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

            if (Pages.ContainsKey(page.Type))
            {
                Pages[page.Type] = pageInfo;
            }
            else
            {
                Pages.Add(page.Type, pageInfo);
            }

            return pageInfo;
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

            partInfo.Analysis.RootSelector = part?.Meta?.Selector;
            partInfo.Analysis.Type = part.Type;

            Parts.Add(part.Type, partInfo);
            return partInfo;
        }

        private static ISelectorFinder GetSelectorFinder(IDictionary<string, string> elements)
        {
            // todo: rework for DI
            // todo: remove as it is runtime context
            return elements == null
                ? new NullSelectorFinder() as ISelectorFinder
                : new SelectorFinder(elements);
        }
    }
}
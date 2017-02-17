using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            var pageInfo = new PageInfo
            {
                App = app,
                Page = page
            };

            _pages.Add(page.Type, pageInfo);

            return pageInfo;
        }

        public static PartInfo AddPart(Metadata<Part> part)
        {
            var partInfo = new PartInfo { Part = part };
            _parts.Add(part.Type, partInfo);
            return partInfo;
        }
    }

    public class AppInfo
    {
        private readonly IList<Metadata<Page>> _pages = new List<Metadata<Page>>();

        public Metadata<Page>[] Pages => _pages.ToArray();

        public Metadata<App> App { get; set; }

        public void AddPage(Metadata<Page> page)
        {
            _pages.Add(page);
        }
    }

    public class PageInfo
    {
        public Metadata<App> App { get; set; }
        public Metadata<Page> Page { get; set; }

        public string GetPageUrl()
        {
            return $"{App.Meta.Url}/{Page.Meta.Url}"
                .Replace("//", "/");
        }

        public void AddField(FieldInfo field)
        {
        }

        public void AddProperty(PropertyInfo property)
        {

        }
    }

    public class PartInfo
    {
        public Metadata<Part> Part { get; set; }
    }
}
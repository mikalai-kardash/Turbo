using System;
using Turbo.Cache.Info;
using Turbo.Metadata;

namespace Turbo.Cache
{
    public class InfoProvider : IInfoProvider
    {
        private readonly IMetadataLoader _metadata;

        public InfoProvider(IMetadataLoader metadata)
        {
            _metadata = metadata;
        }

        public PageInfo GetPageInfo<TPage>(AppInfo appInfo)
        {
            PageInfo pageInfo;
            if (TurboSync.TryGetPage<TPage>(out pageInfo))
            {
                return pageInfo;
            }

            var pageMeta = _metadata.GetPageMeta<TPage>();
            return TurboSync.AddPage(appInfo.App, pageMeta);
        }

        public AppInfo GetAppInfo<TApp>()
        {
            AppInfo appInfo;
            if (TurboSync.TryGetApp<TApp>(out appInfo))
            {
                return appInfo;
            }

            var appMeta = _metadata.GetAppMeta<TApp>();
            return TurboSync.AddApp(appMeta);
        }

        public PartInfo GetPartInfo(Type partType)
        {
            PartInfo partInfo;
            if (TurboSync.TryGetPart(partType, out partInfo))
            {
                return partInfo;
            }

            var meta = _metadata.GetPartMeta(partType);
            return TurboSync.AddPart(meta);
        }

        public PageInfo GetPageInfo(Type pageType)
        {
            PageInfo pageInfo;
            TurboSync.TryGetPage(pageType, out pageInfo);
            return pageInfo;
        }
    }
}
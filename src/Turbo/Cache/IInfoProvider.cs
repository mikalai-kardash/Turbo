using System;
using Turbo.Cache.Info;

namespace Turbo.Cache
{
    public interface IInfoProvider
    {
        PageInfo GetPageInfo<TPage>(AppInfo appInfo);
        AppInfo GetAppInfo<TApp>();

        PartInfo GetPartInfo(Type partType);
        PageInfo GetPageInfo(Type pageType);
    }
}
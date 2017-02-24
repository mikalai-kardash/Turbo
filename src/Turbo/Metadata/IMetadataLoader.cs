using System;
using Turbo.Metadata.Models;

namespace Turbo.Metadata
{
    public interface IMetadataLoader
    {
        AppMeta<T> GetAppMeta<T>();
        PageMeta<T> GetPageMeta<T>();
        PartMeta<T> GetPartMeta<T>();

        Metadata<App> GetAppMeta(Type appType);
        Metadata<Part> GetPartMeta(Type partType);
        Metadata<Page> GetPageMeta(Type pageType);
    }
}
using System;
using Turbo.Metadata.Models;

namespace Turbo.Metadata
{
    public interface IMetadataLoader
    {
        AppMeta<T> GetAppMeta<T>();
        PageMeta<T> GetPageMeta<T>();
        PartMeta<T> GetPartMeta<T>();

        Metadata<Part> GetPartMeta(Type partType);
        Metadata<Page> GetPageMeta(Type pageType);
    }
}
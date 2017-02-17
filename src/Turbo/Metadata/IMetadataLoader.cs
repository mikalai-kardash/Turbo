namespace Turbo.Metadata
{
    public interface IMetadataLoader
    {
        AppMeta<T> GetAppMeta<T>();
        PageMeta<T> GetPageMeta<T>();
        PartMeta<T> GetPartMeta<T>();
    }
}
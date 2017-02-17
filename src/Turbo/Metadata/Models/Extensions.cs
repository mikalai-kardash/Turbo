namespace Turbo.Metadata.Models
{
    public static class Extensions
    {
        public static AppMeta<T> ToMeta<T>(this App app)
        {
            return new AppMeta<T>(app);
        }

        public static PageMeta<T> ToMeta<T>(this Page page)
        {
            return new PageMeta<T>(page);
        }

        public static PartMeta<T> ToMeta<T>(this Part part)
        {
            return new PartMeta<T>(part);
        }
    }
}
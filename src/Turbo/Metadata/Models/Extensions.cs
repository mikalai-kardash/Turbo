using System;

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

        public static Metadata<Part> ToMeta(this Part part, Type partType)
        {
            var type = typeof(PartMeta<>).MakeGenericType(partType);
            return (Metadata<Part>) Activator.CreateInstance(type, part);
        }

        public static Metadata<Page> ToMeta(this Page page, Type pageType)
        {
            var type = typeof(PageMeta<>).MakeGenericType(pageType);
            return (Metadata<Page>) Activator.CreateInstance(type, page);
        }
    }
}
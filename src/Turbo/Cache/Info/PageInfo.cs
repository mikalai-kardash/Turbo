using Turbo.Construction;
using Turbo.Metadata;
using Turbo.Metadata.Models;

namespace Turbo.Cache.Info
{
    public class PageInfo
    {
        public PageInfo()
        {
            Analysis = new Analysis();
        }

        public Analysis Analysis { get; }
        public Metadata<App> App { get; set; }
        public Metadata<Page> Page { get; set; }
        public ISelectorFinder Finder { get; set; }

        public string GetPageUrl()
        {
            return 
                $"{App.Meta.Url}/{Page.Meta.Url}"
                .Replace("//", "/");
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Turbo.Metadata;
using Turbo.Metadata.Models;

namespace Turbo.Cache.Info
{
    public class AppInfo
    {
        private readonly IList<Metadata<Page>> _pages
            = new List<Metadata<Page>>();

        public Metadata<Page>[] Pages => _pages.ToArray();

        public Metadata<App> App { get; set; }

        public void AddPage(Metadata<Page> page)
        {
            _pages.Add(page);
        }
    }
}
using System;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.DI;

namespace Turbo.Construction
{
    public class PageAnalyzer : BaseAnalyzer, IPageAnalyzer
    {
        private readonly IInfoProvider _infoProvider;

        private PageInfo _page;

        public PageAnalyzer(IObjectFactory objectFactory, IInfoProvider infoProvider)
            : base(objectFactory)
        {
            _infoProvider = infoProvider;
        }

        public PageInfo Analyze(Type pageType)
        {
            _page = _infoProvider.GetPageInfo(pageType);
            if (!_page.Analysis.IsDone)
            {
                TypeAnalyzer.Analyze(pageType, this);
            }
            return _page;
        }

        public override Analysis Analysis => _page.Analysis;

        public override ISelectorFinder Finder => _page.Finder;
    }
}
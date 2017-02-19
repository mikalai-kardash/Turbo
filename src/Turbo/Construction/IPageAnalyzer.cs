using System;
using Turbo.Cache.Info;

namespace Turbo.Construction
{
    public interface IPageAnalyzer
    {
        PageInfo Analyze(Type pageType);
    }
}
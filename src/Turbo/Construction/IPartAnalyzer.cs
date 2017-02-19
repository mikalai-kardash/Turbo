using System;
using Turbo.Cache.Info;

namespace Turbo.Construction
{
    public interface IPartAnalyzer
    {
        PartInfo Analyze(Type partType);
    }
}
using System;
using System.Collections.Generic;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.Construction.Target;
using Turbo.DI;

namespace Turbo.Construction
{
    public class PartAnalyzer : BaseAnalyzer, IPartAnalyzer
    {
        private static readonly HashSet<string> RootNames =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "root",
                "_root",
                "container",
                "_container"
            };

        private readonly IInfoProvider _infoProvider;

        private PartInfo _part;

        public PartAnalyzer(IObjectFactory objectFactory, IInfoProvider infoProvider)
            : base(objectFactory)
        {
            _infoProvider = infoProvider;
        }

        public PartInfo Analyze(Type partType)
        {
            _part = _infoProvider.GetPartInfo(partType);
            if (!_part.Analysis.IsDone)
            {
                var waitSelector = _part.Part?.Meta?.Wait;
                if (!string.IsNullOrWhiteSpace(waitSelector))
                {
                    _part.Analysis.WaitForElement(waitSelector);
                }

                TypeAnalyzer.Analyze(partType, this);

                _part.Analysis.IsDone = true;
            }
            return _part;
        }

        public override Analysis Analysis => _part.Analysis;

        public override ISelectorFinder Finder => _part.Finder;

        public override void SetWebElement(ITarget target)
        {
            var name = target.Name;
            if (RootNames.Contains(name))
            {
                Analysis.AssignRoot(target);
                return;
            }

            base.SetWebElement(target);
        }
    }
}
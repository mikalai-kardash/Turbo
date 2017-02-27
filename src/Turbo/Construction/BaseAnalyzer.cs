using System;
using Turbo.Cache;
using Turbo.Cache.Info;
using Turbo.Construction.Target;
using Turbo.DI;

namespace Turbo.Construction
{
    public abstract class BaseAnalyzer : IPageBuilder
    {
        private readonly IObjectFactory _objectFactory;

        protected BaseAnalyzer(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public abstract Analysis Analysis { get; }

        public abstract ISelectorFinder Finder { get; }

        public virtual void SetWebDriver(ITarget target)
        {
            Analysis.AssignWithWebDriver(target);
        }

        public virtual void SetWebElement(ITarget target)
        {
            var selector = Finder.GetSelector(target.Name);
            Analysis.Assign(target, selector);
        }

        public virtual void SetWebElementCollection(ITarget target)
        {
            var selector = Finder.GetSelector(target.Name);
            Analysis.AssignCollection(target, selector);
        }

        public virtual void SetPart(ITarget target)
        {
            var partInfo = AnalyzePart(target.TargetType);
            if (partInfo.Part.Meta.Immediate)
            {
                Analysis.AssignPart(target, partInfo);
            }
        }

        public virtual void SetPartCollection(ITarget target)
        {
            var partInfo = AnalyzePart(target.GetTypeOfArray());
            Analysis.AssignPartCollection(target, partInfo);
        }

        private PartInfo AnalyzePart(Type partType)
        {
            var partAnalyzer = _objectFactory.GetInstance<IPartAnalyzer>();
            var partInfo = partAnalyzer.Analyze(partType);
            return partInfo;
        }
    }
}
using Turbo.Construction.Target;

namespace Turbo.Construction
{
    public interface IPageBuilder
    {
        void SetWebDriver(ITarget target);

        void SetWebElement(ITarget target);
        void SetWebElementCollection(ITarget target);

        void SetPart(ITarget target);
        void SetPartCollection(ITarget target);
    }
}
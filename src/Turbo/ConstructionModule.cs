using Turbo.Construction;
using Turbo.DI;

namespace Turbo
{
    public class ConstructionModule : Module
    {
        public ConstructionModule()
        {
            Registry.AddType<IPageAnalyzer, PageAnalyzer>();
            Registry.AddType<IPartAnalyzer, PartAnalyzer>();
            Registry.AddType<IPageFactory, PageFactory>();
        }
    }
}
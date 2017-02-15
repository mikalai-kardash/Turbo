using System.Reflection;

namespace Turbo
{
    public class TurboConfiguration
    {
        public IObjectFactory ObjectFactory { get; set; }
        public Assembly[] Assemblies { get; set; }

        public static TurboConfiguration Default { get; } = new TurboConfiguration
        {
            Assemblies = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.GetCallingAssembly(),
                Assembly.GetEntryAssembly()
            },
            ObjectFactory = new DefaultObjectFactory()
        };
    }
}
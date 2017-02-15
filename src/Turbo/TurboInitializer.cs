namespace Turbo
{
    public static class TurboInitializer
    {
        public static TurboFactory Init()
        {
            return Init(TurboConfiguration.Default);
        }

        public static TurboFactory Init(TurboConfiguration configuration)
        {
            return new TurboFactory(configuration);
        }
    }
}
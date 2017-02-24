namespace Turbo.Logging
{
    public static class Extensions
    {
        public static void Information(this ILogger logger, int eventId, string message)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.Log(LogLevel.Information, eventId, message, null, null);
            }
        }

        public static void Trace(this ILogger logger, int eventId, string message, params object[] args)
        {
        }
    }
}
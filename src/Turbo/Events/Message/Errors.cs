using System;

namespace Turbo.Events.Message
{
    public static class Errors
    {
        public static Action<T> GetError<T>(Func<T, string> getMessage)
        {
            return t =>
            {
                throw new InvalidOperationException(getMessage(t));
            };
        }
    }
}
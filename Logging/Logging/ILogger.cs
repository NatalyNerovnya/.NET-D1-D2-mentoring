namespace Logging
{
    using System;

    public interface ILogger
    {
        void LogError(Exception exception, string message);

        void LogInfo(string message);

        void LogDebug(string message);
    }
}

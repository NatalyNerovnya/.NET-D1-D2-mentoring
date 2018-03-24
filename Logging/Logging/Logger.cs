namespace Logging
{
    using System;

    using NLog;

    public class Logger: ILogger
    {
        private static NLog.ILogger logger;
        public Logger()
        {
            logger = LogManager.GetLogger("Logger");
        }
        public void LogError(Exception exception, string message)
        {
            logger.Error(exception, message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
    }
}

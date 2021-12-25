using NLog;

namespace CharlotteBackendManagement.Helper
{
    public static class LoggerHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogerInfo(string infoMessage)
        {
            Logger.Info(infoMessage);
        }

        public static void LogerDeBug(string debugMessage)
        {
            Logger.Debug(debugMessage);
        }

        public static void LogerError(Exception ex) 
        {
            Logger.Error(ex);
        }
    }
}

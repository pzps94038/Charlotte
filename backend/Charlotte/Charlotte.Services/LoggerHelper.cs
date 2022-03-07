
using NLog;

namespace Charlotte.Services
{
    public static class LoggerHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Info(string infoMessage)
        {
            Logger.Info(infoMessage);
        }

        public static void DeBug(string debugMessage)
        {
            Logger.Debug(debugMessage);
        }

        public static void Error(Exception ex) 
        {
            Logger.Error(ex);
        }
    }
}

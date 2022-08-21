
using NLog;

namespace Charlotte.Services
{
    public class LoggerUtils
    {
        private static Logger _Logger;
        private static object _lock = new object();

        public static void Info(string infoMessage)
        {
            GetLogger().Info(infoMessage);
        }

        public static void DeBug(string debugMessage)
        {
            GetLogger().Debug(debugMessage);
        }

        public static void Error(Exception ex) 
        {
            GetLogger().Error(ex);
        }

        public static Logger GetLogger() 
        {
            lock (_lock)
            {
                if (_Logger == null)
                {
                    _Logger = LogManager.GetCurrentClassLogger();
                }
                return _Logger;
            }
        }
    }
}

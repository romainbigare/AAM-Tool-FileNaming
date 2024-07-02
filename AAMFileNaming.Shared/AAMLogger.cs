using NLog;

namespace AAMFileNaming.Shared.Logging
{

    public static class AAMLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Info(string info) => Logger.Info(info);

        public static void Error(Exception ex, string message) => Logger.Error(ex, message);  

        public static void Error(string message) => Logger.Error(message);
    }
}

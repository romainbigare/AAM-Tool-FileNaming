using NLog;

namespace AAMFileNaming.Shared.Logging
{

    public static class AAMLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Info(string info)
        {
            try
            {
                Logger.Info(info);
            }
            catch(Exception e)
            {
                // ignored
                Console.WriteLine(e);   
            }
        }

        public static void Error(Exception ex, string message) => Logger.Error(ex, message);  

        public static void Error(string message) => Logger.Error(message);
    }
}

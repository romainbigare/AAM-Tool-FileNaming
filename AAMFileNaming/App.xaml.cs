using AAMFileNamingCore.UI;
using NLog;
using System;
using System.Windows;

namespace AAMFileNaming
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string folderPath = null;

            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                Logger.Error(ex.ExceptionObject as Exception, "Unhandled exception");
            };

            if (e.Args.Length > 0)
            {
                folderPath = e.Args[0];
                Logger.Info($"User opened the app with folder path: {folderPath}");
            }

            try
            {
                var result = AutoUpdater.Update();
                Logger.Info($"Updater result : {result}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error while updating the app");
            }

            // show window
            LoadWindow(folderPath);
        }

        private void LoadWindow(string folderPath)
        {
            MainWindow mainWindow = new MainWindow(folderPath);
            mainWindow.Show();
        }
    }
}

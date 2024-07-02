using AAMFileNaming.Shared.Logging;
using NLog;
using System;
using System.Globalization;
using System.Windows;

namespace AAMFileNaming
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CoreAssemblyLoader loader = new CoreAssemblyLoader();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string folderPath = null;
            var updateTimeStamp = $"{AppDomain.CurrentDomain.BaseDirectory}LastUpdated.txt";
            string format = "yyyy-MM-dd_HH-mm-ss";

            // subscribe to app closing
            this.Exit += (s, ex) =>
            {
                try
                {
                    // write simple text file in current app folder with timestamp
                    var timestamp = DateTime.Now.ToString(format);
                    System.IO.File.WriteAllText(updateTimeStamp, timestamp);
                }
                catch (Exception excep)
                {
                    AAMLogger.Error(excep, "Error saving time stamp");
                }

                AAMLogger.Info("App closing");
            }; 

            try
            {
                string? lastUpdate = null;
                // check last time the app was opened
                if (System.IO.File.Exists(updateTimeStamp))
                {
                    lastUpdate = System.IO.File.ReadAllText(updateTimeStamp);
                    AAMLogger.Info($"Last update: {lastUpdate}");
                }

                // if last update is never or more than 1 day ago, check for updates
                if (lastUpdate == null || DateTime.Now.Subtract(DateTime.ParseExact(lastUpdate, format, CultureInfo.InvariantCulture)).TotalDays > 1)
                {
                    var result = AutoUpdater.Update();
                    AAMLogger.Info($"Updater result : {result}");
                }
            }
            catch (Exception excep)
            {
                AAMLogger.Error(excep, "Error while updating the app");
            }

            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                AAMLogger.Error(ex.ExceptionObject as Exception, "Unhandled exception");
            };

            if (e.Args.Length > 0)
            {
                folderPath = e.Args[0];
            }

            // show window
            LoadWindow(folderPath);
        }

        private void LoadWindow(string folderPath)
        {
            try
            {
                loader.LoadCoreAssembly();
                var mainWindow = loader.CreateMainWindow(folderPath);
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                AAMLogger.Error(ex, "Error while loading the main window");
            }
        }
    }
}

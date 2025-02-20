using AAMFileNaming.Shared.Logging;
using NLog;
using System;
using System.Globalization;
using System.Windows;
using System.Threading.Tasks;

namespace AAMFileNaming
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CoreAssemblyLoader loader = new CoreAssemblyLoader();

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            string folderPath = null;
            var updateTimeStamp = $"{AppDomain.CurrentDomain.BaseDirectory}LastUpdated.txt";
            string format = "yyyy-MM-dd_HH-mm-ss";

            // subscribe to app closing
            this.Exit += (s, ex) =>
            {
                AAMLogger.Info("App closing");
            };


            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                AAMLogger.Error(ex.ExceptionObject as Exception, "Unhandled exception");
            };

            if (e.Args.Length > 0)
            {
                folderPath = e.Args[0];
            }

            // show window
            await LoadWindow(folderPath);

            try
            {
                Task.Run(() =>
                {
                    var result = AutoUpdater.Update();
                    AAMLogger.Info(result);
                });
            }
            catch (Exception excep)
            {
                AAMLogger.Error(excep, "Error while updating the app");
            }
        }

        private async Task LoadWindow(string folderPath)
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

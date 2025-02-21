using AAMFileNaming.Shared.Logging;
using AAMFileNamingCore.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Expression = System.Linq.Expressions.Expression;

namespace AAMFileNaming
{
    internal class CoreAssemblyLoader
    {
        private AssemblyLoadContext? _loadContext;
        private ObjectActivator? _activator;

        public Window CreateMainWindow(string folderPath)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (_activator == null)
            {
                var coreAssembly = _loadContext?.Assemblies.FirstOrDefault();
                if (coreAssembly == null)
                {
                    throw new InvalidOperationException("Core assembly not found.");
                }

                var mainWindowType = coreAssembly.GetType("AAMFileNamingCore.UI.MainWindow");
                if (mainWindowType == null)
                {
                    throw new InvalidOperationException("Main window type not found in core assembly.");
                }

                // Get the constructor with a string parameter
                ConstructorInfo ctor = mainWindowType.GetConstructor(new[] { typeof(string) });
                if (ctor == null)
                {
                    throw new InvalidOperationException("Constructor not found.");
                }


                // Compile the constructor into a delegate and cache it
                _activator = ActivatorFactory.CreateActivator(ctor);
            }

            watch.Stop();
            Debug.WriteLine($"Step 3: {watch.ElapsedMilliseconds} ms");

            // Use the compiled delegate to create the instance
            return (Window)_activator(folderPath);

        }

        public Window CreateMainWindowSs(string folderPath)
        {
            var coreAssembly = _loadContext?.Assemblies.FirstOrDefault();

            if (coreAssembly == null)
            {
                AAMLogger.Error("Core assembly not found");
                MessageBox.Show("Core assembly not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // get main window from core assembly
            var mainWindowType = coreAssembly.GetType("AAMFileNamingCore.UI.MainWindow");
            if (mainWindowType == null)
            {
                AAMLogger.Error("Main window not found in core assembly");
                MessageBox.Show("Main window not found in core assembly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
          
            var mainWindow = Activator.CreateInstance(mainWindowType, folderPath) as Window;
            if (mainWindow == null)
            {
                AAMLogger.Error("Error while creating main window");
                MessageBox.Show("Error while creating main window", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

           
            return mainWindow;
        }

        public void LoadCoreAssembly()
        {

            try
            {
                var currentAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                if (string.IsNullOrEmpty(currentAssemblyPath))
                {
                    AAMLogger.Error("Current assembly path not found");
                    MessageBox.Show("Current assembly path not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return ;
                }
                // path of core assembly
                var coreAssemblyPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(currentAssemblyPath), "AAMFileNamingCore.dll");
                if (string.IsNullOrEmpty(coreAssemblyPath))
                {
                    AAMLogger.Error("Core assembly path not found");
                    MessageBox.Show("Core assembly path not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                _loadContext = new AssemblyLoadContext(name: "AAMFileNamingCore", isCollectible: true);
                var a = _loadContext.LoadFromAssemblyPath(coreAssemblyPath);

                return;
            }
            catch (Exception ex)
            {
                AAMLogger.Error(ex, "Error while loading the assembly");
                return;
            }
        }

        public void UnloadCoreAssembly()
        {
            try
            {
                _loadContext?.Unload();
                _loadContext = null;
                // wait for the GC to collect the assembly
                GC.Collect();
                GC.WaitForPendingFinalizers();
                // wait 0.5 seconds more to make sure
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                AAMLogger.Error(ex, "Error while unloading the assembly");
            }
        }
    }

    public delegate object ObjectActivator(string folderPath);

    public static class ActivatorFactory
    {
        public static ObjectActivator CreateActivator(ConstructorInfo ctor)
        {
            // Define a parameter for the lambda (in this case, a single string argument)
            ParameterExpression param = Expression.Parameter(typeof(string), "folderPath");

            // Create the expression that represents "new MainWindow(folderPath)"
            NewExpression newExp = Expression.New(ctor, param);

            // Compile the lambda into a delegate
            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator), newExp, param);

            // Return the compiled delegate
            return (ObjectActivator)lambda.Compile();
        }
    }
}

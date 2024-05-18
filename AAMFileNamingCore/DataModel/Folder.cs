using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;

namespace AAMFileNamingCore.DataModel
{
    public class Folder : INotifyPropertyChanged
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Dispatcher UIDispatcher { get; set; }
        public string Name => System.IO.Path.GetFileName(Path);

        public string Count => Files.Count > 0 ? $"{Files?.Count.ToString()} files in folder" : Path != "No path selected" ? $"No files in selected folder" : "No folder selected";

        private string _path = "No path selected";
        public string Path { get => _path; set { _path = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path")); } }

        public ObservableCollection<File> Files { get; set; } = new ObservableCollection<File>();

        public Folder(Dispatcher dispatcher)
        {
            UIDispatcher = dispatcher;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChangeTarget(string target, bool subfolders, List<string> filters, Action<double> updateProgress)
        {
            UIDispatcher.Invoke(() => Files.Clear());
            Path = target;
            int count = 0;

            try
            {
                string[] extensionsToClear = new string[] { ".lnk", ".bak", ".dat", ".skb", ".rws", ".3dmbak", ".000", ".db", ".slog", ".tmp", ".css", ".log" }; // add extensions to clear here

                var files = System.IO.Directory.GetFiles(target, "", subfolders ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);

                // sort by extension and then by name
                files = files.OrderBy(f => System.IO.Path.GetExtension(f)).ThenBy(f => System.IO.Path.GetFileName(f)).ToArray();

                int max = files.Length;
                foreach (var file in files)
                {
                    // check file visibility
                    if (System.IO.Path.GetFileName(file).StartsWith("~$"))
                        continue;

                    if (extensionsToClear.Contains(System.IO.Path.GetExtension(file)))
                        continue;

                    if (string.IsNullOrEmpty(System.IO.Path.GetExtension(file)))
                        continue;

                    if (filters != null && filters.Count > 0 && !filters.Contains(System.IO.Path.GetExtension(file)))
                        continue;

                    // check if file is hidden
                    if ((System.IO.File.GetAttributes(file) & System.IO.FileAttributes.Hidden) == System.IO.FileAttributes.Hidden)
                        continue;

                    UIDispatcher.Invoke(() => Files.Add(new File(file)));
                    count++;
                    updateProgress(count / (double)max * 80 + 20);
                }

                Logger.Info($"{count} files found in folder {target}");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Logger.Error(e, "Error while reading files");
            }

            OnPropertyChanged("Count");

        }
    }
}

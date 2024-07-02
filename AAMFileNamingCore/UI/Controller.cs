using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AAMFileNamingCore.DataModel;
using AAMFileNaming.Shared.Logging;

namespace AAMFileNamingCore.UI
{
    public enum NamingProtocol
    {
        Iso19650,
        NonIso,
        ForComments
    }

    public class Controller : INotifyPropertyChanged
    {
        public Folder Folder { get; set; }
        public MainWindow UI { get; set; }

        public string NoFilesWarning => Folder.Path == null || Folder.Path == "No path selected" ? "No folder selected" : IncludeSubfolders() == false ? "No files found. \nInclude subfolders to see more." : "No files found in folder or subfolders.";

        public Controller(MainWindow ui, string folderPath)
        {
            UI = ui;
            InitFolder(folderPath);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitFolder(string folderPath)
        {
            AAMLogger.Info("Initializing folder");
            Folder = new Folder(UI.Dispatcher);
            if (folderPath != null)
                SetFolder(folderPath, false, null);
        }

        public async void UpdateProgress(double progress)
        {
            UI.Dispatcher.Invoke(() =>
            {
                UI.ProgressBar.Value = progress;
            });
        }

        private async void ProgressButtonInit()
        {
            UI.Dispatcher.Invoke(() =>
            {
                UI.BrowseButton.Visibility = Visibility.Collapsed;
                UI.ProgressBar.Visibility = Visibility.Visible;
                UI.ProgressBar.Value = 20;
                UI.ProgressBarGrid.Visibility = Visibility.Visible;
            });
        }

        private async Task ProgressButtonComplete()
        {
            if (Folder?.Files?.Count > 0)
            {
                UI.Dispatcher.Invoke(() =>
                {
                    UI.BrowseButton.Visibility = Visibility.Visible;
                    UI.ProgressBar.Visibility = Visibility.Collapsed;
                    UI.ProgressBar.Value = 0;
                    UI.ProgressBarGrid.Visibility = Visibility.Collapsed;
                });
            }

            else
            {
                // need to pretend we're loading something to show the progress bar for 2 seconds
                UI.Dispatcher.Invoke(() =>
                {
                    UI.ProgressBar.Value = 50;
                    UI.ProgressBar.Visibility = Visibility.Visible;
                    UI.ProgressBarGrid.Visibility = Visibility.Visible;
                });

                await Task.Delay(1000);

                UI.Dispatcher.Invoke(() =>
                {
                    UI.BrowseButton.Visibility = Visibility.Visible;
                    UI.ProgressBar.Visibility = Visibility.Collapsed;
                    UI.ProgressBar.Value = 0;
                    UI.ProgressBarGrid.Visibility = Visibility.Collapsed;
                });
            }

        }

        public async void SetFolder(string? path, bool subfolders, List<string> filters = null)
        {
            AAMLogger.Info($"Setting folder to {path}, include subfolders : {subfolders}");

            RefreshUILayout(isLoading: true);
            ProgressButtonInit();
            await Task.Delay(100);

            try
            {

                if (path != null)
                {
                    var parts = path.Split("\\");
                    if(path.StartsWith("X") && parts.Count() < 4)
                    {
                        MessageBox.Show("Bulk renaming on this folder is forbidden.", "Access forbidden", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        await Task.Run(() => Folder.ChangeTarget(path, subfolders, filters, UpdateProgress));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AAMLogger.Error(ex, "Error while setting folder");
            }

            AAMLogger.Info($"Folder set to {path}");
            await ProgressButtonComplete();
            RefreshUILayout(isLoading: false);
            return;
        }

        internal void BrowseToFile()
        {
            // browse to directory dialog
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetFolder(dialog.SelectedPath, IncludeSubfolders(), null);
            }
        }

        internal void InputWindow(Type input)
        {
            DataInput dataInput = new DataInput(input, this);

            // show where the click was processed 
            dataInput.Owner = UI;
            dataInput.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dataInput.ShowDialog();

            if (dataInput.SaveResult)
            {
                // TO DO : Save the data to the controller
                var selectedFiles = GetGridSelection();
                Task.Run(() => SaveToSelected(dataInput, selectedFiles?.ToList()));
            }
        }

        private void SaveToSelected(DataInput dataInput, List<File> selectedFiles)
        {
            foreach (var file in selectedFiles)
            {
                file.UpdateValue(dataInput);
            }
        }

        internal ICollection<File> GetGridSelection()
        {
            var prot = SelectedNamingProtocol();
            if (prot == NamingProtocol.Iso19650)
            {
                var sel = UI.DatagridIso.SelectedItems;
                try
                {
                    return sel.Cast<File>().ToList();
                }
                catch (Exception ex)
                {
                    AAMLogger.Error(ex, "Error while getting selected files");
                    return new List<File>();
                }
            }
            else if( prot == NamingProtocol.NonIso)
            {
                var sel = UI.DatagridNonIso.SelectedItems;
                try
                {
                    return sel.Cast<File>().ToList();
                }
                catch (Exception ex)
                {
                    AAMLogger.Error(ex, "Error while getting selected files");
                    return new List<File>();
                }
            }
            else if(prot == NamingProtocol.ForComments)
            {
                var sel = UI.DatagridForComments.SelectedItems;
                try
                {
                    return sel.Cast<File>().ToList();
                }
                catch (Exception ex)
                {
                    AAMLogger.Error(ex, "Error while getting selected files");
                    return new List<File>();
                }
            }
            else
            {
                return new List<File>();
            }
        }

        private NamingProtocol SelectedNamingProtocol()
        {
            var selected = UI.NamingProtocol?.SelectedItem;
            if (selected == null)
                return NamingProtocol.Iso19650;

            var content = (selected as ComboBoxItem)?.Content;
            if (content == null)
                return NamingProtocol.Iso19650;

            var ct = content.ToString();
            if(ct == null)
                return NamingProtocol.Iso19650;

            if(ct.Contains("19650"))
                return NamingProtocol.Iso19650;
            if(ct.ToLower().Contains("comment"))
                return NamingProtocol.ForComments;  
            else
                return NamingProtocol.NonIso;
        }

        internal bool ValidateInput(string text, string tagString)
        {
            if (tagString == "Volume")
                return text.Length == 1 || text.Length == 2;

            if (tagString == "DrawingSerialNo")
                return text.Length == 3;

            else
                return true;
        }

        internal void ApplyInput(string text, TextBox textBox, ICollection<File> selectedFiles)
        {
            // find all selected row in current datagrid
            var prop = textBox?.Tag?.ToString();

            if (selectedFiles.Count == 0 || prop == null)
                return;

            if (prop.Contains("Serial"))
            {
                // convert text to int
                if (!int.TryParse(text, out int digit))
                    return;

                foreach (var file in selectedFiles)
                {
                    file.UpdateValue(prop, digit.ToString("D3"));
                    digit++;
                }

                return;
            }

            // split text by space
            var parts = text.Split(" ");

            // every first letter must be uppercase
            for (int i = 0; i < parts.Length; i++)
            {
                if(parts[i].Length > 0)
                    parts[i] = parts[i].First().ToString().ToUpper() + parts[i].Substring(1);
            }

            // join parts by space
            text = string.Join("", parts);
            text = text.Replace("_", "-");
            text = text.Replace(".", "-");

            foreach (var file in selectedFiles)
            {
                file.UpdateValue(prop, text);
            }
        }

        public async Task EnsureUniqueness(ICollection<File> selectedFiles = null)
        {
            // if selected files is not passed, get all files in the current folder
            if (selectedFiles == null)
                selectedFiles = GetGridSelection();

            bool iso = SelectedNamingProtocol() == NamingProtocol.Iso19650;
            try
            {
                foreach (var file in selectedFiles)
                {
                    file.EnsureUniqueness(Folder.Files, iso);
                }
            }
            catch (Exception ex)
            {
                AAMLogger.Error(ex, "Error while ensuring uniqueness");
                MessageBox.Show(ex.Message);
            }
        }

        internal void Save()
        {
            var selection = GetGridSelection();
            var protocol = SelectedNamingProtocol();
            bool iso = protocol == NamingProtocol.Iso19650;
            bool forComments = protocol == NamingProtocol.ForComments;

            if (selection == null)
            {
                AAMLogger.Info("No files selected");
                MessageBox.Show("No files selected");
                return;
            }

            var newPathProp = iso ? "NewPathIso" : forComments ? "NewPathForComments" : "NewPathNonIso";
            var filteredSelection = selection.Where(f => f.Path != f.GetType().GetProperty(newPathProp)?.GetValue(f)?.ToString()).ToList();
            var grouped = filteredSelection.GroupBy(f => f.GetType().GetProperty(newPathProp)?.GetValue(f)?.ToString()).ToList();

            foreach (var group in grouped)
            {
                if (group.Count() > 1)
                {
                    int i = 0;
                    foreach (var file in group)
                    {
                        if(i>0)
                            file.UpdateValue("Description", $"{file.GetType().GetProperty("Description")?.GetValue(file)?.ToString()} ({i})");
                        
                        i++;
                    }
                }
            }

            var files = grouped.SelectMany(g => g).ToList();

            int count = 0;
            foreach (var file in files)
            {
                if(file.Rename(protocol))
                    count++;
            }

            MessageBox.Show($"{count} files renamed");

        }

        internal bool IncludeSubfolders()
        {
            var selected = UI.FolderSource.SelectedItem as ComboBoxItem;
            var selectedValue = selected?.Content?.ToString()?.ToLower();
            return selectedValue?.Contains("subfolder") ?? false;
        }

        internal void FolderStructureChange()
        {
            if (Folder.Path != null && Folder.Path != "No path selected")
                SetFolder(Folder.Path, IncludeSubfolders(), null);
        }

        internal void FilterByFormat()
        {
            if (Folder.Path == null || Folder.Path == "No path selected")
                return;

            var extensions = Folder.Files.Select(f => f.Extension).Distinct().ToList();

            ExtensionFilter windowDialog = new ExtensionFilter(extensions);

            windowDialog.Owner = UI;
            windowDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            windowDialog.ShowDialog();

            if (windowDialog.IsSaved)
            {
                var selected = windowDialog.SelectedExtensions;
                SetFolder(Folder.Path, true, windowDialog.SelectedExtensions);
            }
        }

        internal async void RefreshUILayout(bool isLoading)
        {
            OnPropertyChanged("NoFilesWarning");
            await UI.Dispatcher.Invoke(async () =>
            {
                if (isLoading)
                {
                    UI.NoFileWarning.Visibility = Visibility.Collapsed;
                }

                else if (Folder.Files.Count == 0)
                {
                    UI.DatagridIso.Visibility = Visibility.Collapsed;
                    UI.DatagridNonIso.Visibility = Visibility.Collapsed;
                    UI.NoFileWarning.Visibility = Visibility.Visible;
                    return;
                }

                var protocol = SelectedNamingProtocol();
                if (protocol == NamingProtocol.Iso19650)
                {
                    UI.DatagridIso.Visibility = Visibility.Visible;
                    UI.DatagridNonIso.Visibility = Visibility.Collapsed;
                    UI.NoFileWarning.Visibility = Visibility.Collapsed;
                    UI.DatagridForComments.Visibility = Visibility.Collapsed;
                }
                else if(protocol == NamingProtocol.NonIso)
                {
                    UI.DatagridIso.Visibility = Visibility.Collapsed;
                    UI.DatagridNonIso.Visibility = Visibility.Visible;
                    UI.NoFileWarning.Visibility = Visibility.Collapsed;
                    UI.DatagridForComments.Visibility = Visibility.Collapsed;

                }
                else
                {
                    UI.DatagridIso.Visibility = Visibility.Collapsed;
                    UI.DatagridNonIso.Visibility = Visibility.Collapsed;
                    UI.NoFileWarning.Visibility = Visibility.Collapsed;
                    UI.DatagridForComments.Visibility = Visibility.Visible;
                }
            });
        }
    }
}

/*/
 * 
    foreach (var file in selection)
    {
        file.Save(iso);
    }

    Logger.Info($"{selection.Count} files renamed, in path {Folder.Path}");

    // TO DO : CHECK UNIQUENESS OF FILENAMES
    // TO DO : IMPLEMENT SAVE
    // something 
    MessageBox.Show("disabled for testing");
/*/

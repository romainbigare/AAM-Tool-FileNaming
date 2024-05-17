using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AAMFileNaming
{
    public class Controller : INotifyPropertyChanged
    {
        public Folder Folder { get; set; } 
        public MainWindow UI { get; set; }

        public string NoFilesWarning => Folder.Path ==null || Folder.Path == "No path selected" ? "No folder selected" : IncludeSubfolders() == false ? "No files found. \nInclude subfolders to see more." : "No files found in folder or subfolders." ;

        public Controller(MainWindow ui)
        {
            UI = ui;
            InitFolder();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitFolder()
        {
            Folder = new Folder(UI.Dispatcher);
        }

        public async void UpdateProgress(double progress)
        {
            UI.Dispatcher.Invoke(() => 
            {
                UI.ProgressBar.Value  = progress;
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
            if(Folder?.Files?.Count > 0)
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
            RefreshUILayout(isLoading:true);
            ProgressButtonInit();
            await Task.Delay(100);

            try
            {
                if (path != null)
                    await Task.Run(() => Folder.ChangeTarget(path,subfolders, filters, UpdateProgress));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            await ProgressButtonComplete();
            RefreshUILayout(isLoading: false);
            return;
        }            

        public void RenameFiles()
        {
            foreach (var file in Folder.Files)
            {
                file.Save();
            }
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
                Task.Run(() => SaveToSelected(input, dataInput.SelectedItem, selectedFiles));
            }
        }

        private void SaveToSelected(Type input, object selectedItem, List<File> selectedFiles)
        {
            foreach (var file in selectedFiles)
            {
                file.UpdateValue(input, selectedItem);
            }
        }

        private List<File> GetGridSelection()
        {
            if(GetCurrentPage().ToLower().Contains("19650"))
            {
                var sel =  UI.DatagridIso.SelectedItems;
                try
                {
                    return sel.Cast<File>().ToList();
                }
                catch (Exception ex)
                {
                    return new List<File>();
                }
            }
            else
            {
                var sel = UI.DatagridNonIso.SelectedItems;
                try
                {
                    return sel.Cast<File>().ToList();
                }
                catch (Exception ex)
                {
                    return new List<File>();
                }
            }
        }

        private string GetCurrentPage()
        {
            var selected = UI.NamingProtocol?.SelectedItem;
            if (selected == null)
                return "";

            var content = (selected as ComboBoxItem)?.Content;
            if (content == null)
                return "";

            return content.ToString();
        }

        internal bool ValidateInput(string text, string tagString)
        {
            if(tagString == "Volume")
                return text.Length == 2;

            if(tagString == "DrawingSerialNo")
                return text.Length == 3;

            else
                return true;
        }

        internal void ApplyInput(string text, TextBox textBox)
        {
            // find all selected row in current datagrid
            var selectedFiles = GetGridSelection();

            if (selectedFiles.Count == 0)
                return;

            foreach (var file in selectedFiles)
            {
                file.UpdateValue(textBox?.Tag?.ToString(), text);
            }
        }

        internal void Save()
        {
            var s = GetGridSelection();
            var file = s.FirstOrDefault();
            if (file == null)
                return;

            // TO DO : CHECK UNIQUENESS OF FILENAMES
            // TO DO : IMPLEMENT SAVE

            MessageBox.Show("disabled for testing");
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

        internal async void RefreshUILayout( bool isLoading)
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

                var page = GetCurrentPage();
                if (page.ToLower().Contains("19650"))
                {
                    UI.DatagridIso.Visibility = Visibility.Visible;
                    UI.DatagridNonIso.Visibility = Visibility.Collapsed;
                    UI.NoFileWarning.Visibility = Visibility.Collapsed;
                }
                else
                {
                    UI.DatagridIso.Visibility = Visibility.Collapsed;
                    UI.DatagridNonIso.Visibility = Visibility.Visible;
                    UI.NoFileWarning.Visibility = Visibility.Collapsed;
                }
            });
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Navigation;
using AAMFileNamingCore.DataModel;
using AAMFileNamingCore.Util;

namespace AAMFileNamingCore.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Controller Controller { get; set; }
        public DebounceHelper DebouncerFast { get; set; } = new DebounceHelper(100);
        public DebounceHelper DebouncerSlow { get; set; } = new DebounceHelper(1200);

        public MainWindow(string folderPath)
        {
            InitializeComponent();
            Controller = new Controller(this, folderPath);
            DataContext = this;
            ToolTipService.SetInitialShowDelay(this, 20); // Adjust as needed
        }

        private void BrowseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.BrowseToFile();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controller?.RefreshUILayout(false);
        }


        private void FilterFormatButton_Click(object sender, RoutedEventArgs e)
        {
            // TO DO : new window to filter by format and clear the tables of elements that don't match
            Controller.FilterByFormat();
        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            // TO DO : DATA INPUT WINDOW SHOW 
            // Volume, Level, DocumentType, Role, TypeCode
            var button = sender as Button;
            if (button == null)
                return;
            var tag = button.Tag;
            if (tag == null)
                return;

            var input = tag.ToString();
            var type = NamingLists.GetType(input);
            if (type == null)
            {
                MessageBox.Show("Error: Type not found.");
                return;
            }

            Controller?.InputWindow(type);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Controller?.Save();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
                return;

            var selection = Controller?.GetGridSelection();

            Action<bool> applyInputAction = (bool isSlowDebounce) =>
            {
                this.Dispatcher.Invoke(() => 
                {
                    var tag = textBox.Tag;
                    var text = textBox.Text;
                    if (tag != null && tag is string tagString)
                    {
                        bool? ok = Controller?.ValidateInput(text, tagString);

                        if (ok.HasValue && ok.Value == false)
                        {
                            textBox.BorderBrush = new LinearGradientBrush()
                            {
                                GradientStops = new GradientStopCollection()
                            {
                                new GradientStop(Colors.IndianRed, 0),
                                new GradientStop(Colors.IndianRed, 1)
                            }
                            };
                        }
                        else
                        {
                            // reset border
                            textBox.BorderBrush = new LinearGradientBrush()
                            {
                                GradientStops = new GradientStopCollection()
                            {
                                new GradientStop(Colors.LightGray, 0),
                                new GradientStop(Colors.LightGray, 1)
                            }
                            };

                            Controller?.ApplyInput(text, textBox, selection);
                        }
                    }
                });
            };

            if (textBox.Tag.ToString().ToLower().Contains("description"))
                DebouncerSlow.Debounce(() => applyInputAction(true));

            else
                DebouncerFast.Debounce(() => applyInputAction(false));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                try
                {
                    textBox.TextChanged -= TextBox_TextChanged;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void FolderSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Controller == null)
                return;

            if (Controller.Folder == null)
                return;

            Controller?.FolderStructureChange();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            bool visible = SideMenuGrid.Width.Value > 100;
            if (visible)
            {
                SideMenuGrid.Width = new GridLength(40);
            }
            else
            {
                SideMenuGrid.Width = new GridLength(250);
            }
        }

        private void UniquenessButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == null)
                return;

            var tag = button.Tag;

            if (tag == null)
                return;

            var input = tag.ToString();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                var website = e.Uri;
                if (website == null) return;
                var url = website.AbsoluteUri;
                if (url == null) return;

                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
    }
}

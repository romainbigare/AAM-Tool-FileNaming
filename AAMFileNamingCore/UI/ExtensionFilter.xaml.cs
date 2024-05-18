using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AAMFileNamingCore.UI
{
    /// <summary>
    /// Interaction logic for ExtensionFilter.xaml
    /// </summary>
    public partial class ExtensionFilter : Window
    {
        public List<string> Extensions { get; set; }
        public List<string> SelectedExtensions { get; set; }
        public bool IsSaved { get; set; } = false;

        public ExtensionFilter()
        {
            InitializeComponent();
        }

        public ExtensionFilter(List<string> extensions)
        {
            Extensions = extensions;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsSaved = false;
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = ListBox.SelectedItems;

            if (selectedItems.Count == 0)
                return;

            SelectedExtensions = new List<string>();
            foreach (var item in selectedItems)
            {
                SelectedExtensions.Add(item.ToString());
            }

            IsSaved = true;
            Close();
        }
    }
}

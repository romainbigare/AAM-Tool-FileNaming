using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AAMFileNaming
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
            this.IsSaved = false;
            this.Close();
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

            this.IsSaved = true;
            this.Close();
        }
    }
}

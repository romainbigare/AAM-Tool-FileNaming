using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DataInput.xaml
    /// </summary>
    public partial class DataInput : Window
    {

        public bool SaveResult { get; set; } = false;
        public object SelectedItem { get; set; } = null;
        public Controller Controller { get; set; }

        public DataInput()
        {
            InitializeComponent();
        }

        public DataInput(Type input, Controller controller)
        {
            // Volume, Level, Role, DocumentType, TypeCode
            InitializeComponent();
            Task.Delay(100).Wait();
            InfoBox.Text = GetTooltip(input);
            Treeview.ItemsSource = GetChoices(input);
            Controller = controller;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private List<TreeViewItem> GetChoices(Type input)
        {
            return NamingLists.GetTreeViewItems(input);
        }

        private string GetTooltip(Type input)
        {
            if (input == typeof(Level))
                return @"Check whether your client has a specific level numbering convention before setting up your drawing register. 

The level code in file names should generally match the levels as shown on drawings.";

            if (input == typeof(Role))
                return @"Always use 'AR' for files produced by AAM Architecture. LA may be used by AAM Landscape.";

            if (input == typeof(DocumentType))
                return @"Use 'DR' for all drawings except Sketches (where 'SK' should be used).";

            if (input == typeof(TypeCode))
                return @"The type code system is derived from the CI/SfB Construction Indexing classification system (also used in Microstation level menu and in AAM library).

For Sketch (SK) drawings, the type code should be 00.";


            return "";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = Treeview.SelectedItem as TreeViewItem;
            if (selectedItem == null)
                return;

            SelectedItem = selectedItem.Tag;
            SaveResult = true;
            // get selected item
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AAMFileNamingCore.DataModel;
using TypeCode = AAMFileNamingCore.DataModel.TypeCode;

namespace AAMFileNamingCore.UI
{
    /// <summary>
    /// Interaction logic for DataInput.xaml
    /// </summary>
    public partial class DataInput : Window
    {
        public Type inputType { get; set; }
        public bool SaveResult { get; set; } = false;
        public object SelectedItem { get; set; } = null;
        public bool manualOverride { get; private set; } = false;
        public Controller Controller { get; set; }
        public string? WindowTitle => inputType?.Name;

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
            inputType = input;
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

            if (input == typeof(DocumentTypeIso))
                return @"Use 'DR' for all drawings except Sketches (where 'SK' should be used).";

            if(input == typeof(DocumentType))
                return @"For letters, agendas, minutes and publicity, a short abbreviation can be used. For other documents leave this field blank.";

            if (input == typeof(TypeCode))
                return @"The type code system is derived from the CI/SfB Construction Indexing classification system (also used in Microstation level menu and in AAM library).
A type code should be used for all drawings (except sketches), schedules and specifications. 
Other documents including sketches should use the type code 00.
";
            if (input == typeof(FileCode))
                return @"File codes represent the topic of the document and come from a pre-defined list.
Working files created in GRAPHICS should use either R (Report) or P (Presentation).
Working files created in ADMIN must contain a file code matching the folder they are created in.
Sketches (SK) should be saved in the project’s WIP folder.
Publicity (PR) files should be saved in the project’s WORK/PR folder.
See the project folder protocols for a visual explanation.
";


            return "";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = Treeview.SelectedItem as TreeViewItem;
            var manualValue = ManualValue.Text;
            if (selectedItem == null && String.IsNullOrEmpty(manualValue))
                return;

            if (String.IsNullOrEmpty(manualValue))
                SelectedItem = selectedItem.Tag;
            else
            {
                if(manualValue.Length > 2)
                {
                    MessageBox.Show("Manual override must be 2 characters or less.");
                    return;
                }
                SelectedItem = manualValue;
                manualOverride = true;
            }

            SaveResult = true;
            // get selected item
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AAMFileNaming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Controller Controller { get; set; } 
        public DebounceHelper Debouncer { get; set; } = new DebounceHelper(300);

        public MainWindow()
        {
            InitializeComponent();
            Controller = new Controller(this);
            DataContext = this;
            ToolTipService.SetInitialShowDelay(this, 200); // Adjust as needed
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
            if(button == null)
                return;
            var tag = button.Tag;
            if(tag == null)
                return;

            var input = tag.ToString();
            var type = NamingLists.GetType(input);
            if(type == null)
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

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                Debouncer.Debounce(() =>
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

                                Controller?.ApplyInput(text, textBox);
                            }
                        }
                    });
                });
            }
        }

        private void FolderSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Controller == null)
                return;

            if(Controller.Folder == null)
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
    }
}

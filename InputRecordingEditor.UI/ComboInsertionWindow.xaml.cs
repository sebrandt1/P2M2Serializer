using InputRecordingEditor.UI.ViewModels;
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

namespace InputRecordingEditor.UI
{
    /// <summary>
    /// Interaction logic for ComboInsertionWindow.xaml
    /// </summary>
    public partial class ComboInsertionWindow : Window
    {
        public ComboInsertionWindow()
        {
            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(Repeat.Text, out var _))
            {
                MessageBox.Show("Repeat was not set to a valid number.");
                return;
            }
            var viewModel = (ComboInsertionViewModel)DataContext;
            try
            {
                viewModel.Result = viewModel.ComboPresets.Presets[PresetSelection.SelectedIndex];
            }
            catch(Exception ex)
            {

            }
            DialogResult = true;
        }
    }
}

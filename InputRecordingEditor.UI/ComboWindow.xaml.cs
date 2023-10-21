using InputRecordingEditor.UI.Converters;
using InputRecordingEditor.UI.FileManaging;
using InputRecordingEditor.UI.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace InputRecordingEditor.UI
{
    /// <summary>
    /// Interaction logic for ComboWindow.xaml
    /// </summary>
    public partial class ComboWindow : Window
    {
        public ComboWindow()
        {
            InitializeComponent();
            DataContext = new ComboPresetViewModel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                return;
            }

            var viewModel = (ComboPresetViewModel)DataContext;
            if(string.IsNullOrEmpty(viewModel.Name))
            {
                MessageBox.Show("Combo preset must have a name!");
                return;
            }
            var comboPreset = ComboPresetConverter.ToComboPreset(viewModel);
            ComboPresetSerializer.Save(comboPreset);
            MessageBox.Show("Saved!");
        }

        private void NewFrameButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                return;
            }

            var viewModel = (ComboPresetViewModel)DataContext;
            viewModel.FrameDataList?.Add(new FrameDataViewModel());
            viewModel.ForceReloadFrameCountText();
        }

        private void FrameDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                var selectedCells = FrameDataGrid.SelectedItems;
                var deletionPrompt = MessageBox.Show($"Are you sure you want to delete {selectedCells.Count} frame{(selectedCells.Count > 1 ? "s" : string.Empty)}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (deletionPrompt == MessageBoxResult.Yes)
                {
                    var viewModel = (P2M2ViewModel)DataContext;
                    var itemsToRemove = selectedCells.Cast<FrameDataViewModel>().ToList();

                    foreach (var item in itemsToRemove)
                    {
                        viewModel.FrameDataList.Remove(item);
                    }

                    viewModel.ForceReloadFrameCountText();
                }
                e.Handled = true;
            }
        }
    }
}

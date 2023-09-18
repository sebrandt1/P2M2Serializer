using InputRecordingEditor.UI.Converters;
using InputRecordingEditor.UI.FileManaging;
using InputRecordingEditor.UI.ViewModels;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using P2M2Serializer.Enums;
using P2M2Serializer.Structs;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InputRecordingEditor.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IP2M2InstanceManager p2m2InstanceManager;

        public MainWindow(IP2M2InstanceManager p2m2InstanceManager)
        {
            this.p2m2InstanceManager = p2m2InstanceManager;
            InitializeComponent();
            var screenWidth = SystemParameters.PrimaryScreenWidth * 0.75;
            var screenHeight = SystemParameters.PrimaryScreenHeight * 0.75;
            Width = screenWidth;
            Height = screenHeight;
        }

        private void FileMenuNew_Click(object sender, RoutedEventArgs e)
        {
            var inputWindow = new NewFileInputParametersWindow();
            if(inputWindow.ShowDialog() == true)
            {
                var author = inputWindow.AuthorTextBox.Text;
                var game = inputWindow.GameTextBox.Text;
                var version = inputWindow.VersionTextBox.Text;
                var useSaveState = inputWindow.UseSaveStateCheckBox.IsChecked ?? false;
                int.TryParse(inputWindow.UndoCountTextBox.Text, out var undoCount);
                var fileName = inputWindow.FileNameTextBox.Text;
                DataContext = p2m2InstanceManager.New(author, game, version, useSaveState, undoCount, fileName);
            }
        }
        private void FileMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = p2m2InstanceManager.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var saveDirectory = "Input Recordings"; //TODO: Convert to const or setting
                var inputRecording = (P2M2ViewModel)DataContext;
                var convertedData = ViewModelConverter.ToP2M2File(inputRecording);
                var fileName = ((P2M2ViewModel)DataContext).FileName + ".p2m2";
                var path = Path.Combine(currentDirectory, saveDirectory, fileName);
                p2m2InstanceManager.Save(path, convertedData);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show($"No p2m2 data to save.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboNew_Click(object sender, RoutedEventArgs e)
        {
            //Open window with textboxes for the fields for a combo preset. (Copy the datagrid for frame display?)
            var comboWindow = new ComboWindow();
            if(comboWindow.ShowDialog() == true)
            {

            }
            //MessageBox.Show($"Not yet implemented");
        }
        private void ComboEdit_Click(object sender, RoutedEventArgs e)
        {
            //Open window with textboxes for the fields for a combo preset. (Copy the datagrid for frame display?)
            var comboWindow = new ComboWindow();
            comboWindow.DataContext = null; //Set context to loaded preset
            MessageBox.Show($"Not yet implemented");
        }
        private void ComboInsert_Click(object sender, RoutedEventArgs e)
        {
            //Load all available combos (title only) and insert it at the user specified frame
            var insertionWindow = new ComboInsertionWindow();
            var viewModel = (P2M2ViewModel)DataContext;
            insertionWindow.DataContext = new ComboInsertionViewModel
            {
                RepeatCombo = 1
            };

            if (insertionWindow.ShowDialog() == true)
            {
                var comboInsertionVm = (ComboInsertionViewModel)insertionWindow.DataContext;
                var result = comboInsertionVm.Result;
                var convertedComboPreset = ComboPresetConverter.ToViewModel(result);
                try
                {
                    for (var i = 0; i < comboInsertionVm.RepeatCombo; i++)
                    {
                        for (var j = 0; j < convertedComboPreset.FrameDataList.Count; j++)
                        {
                            viewModel.FrameDataList.Add(convertedComboPreset.FrameDataList[j]);
                        }
                    }
                    viewModel.ForceReloadFrameCountText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not insert combo, is there a frame recording loaded?");
                }
            }
        }

        private void FrameDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Delete)
            {
                var selectedCells = FrameDataGrid.SelectedItems;
                var deletionPrompt = MessageBox.Show($"Are you sure you want to delete {selectedCells.Count} frame{(selectedCells.Count > 1 ? "s" : string.Empty)}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (deletionPrompt == MessageBoxResult.Yes)
                {
                    var viewModel = (P2M2ViewModel)DataContext;

                    for(var i = 0; i < selectedCells.Count; i++)
                    {
                        var cellIndex = FrameDataGrid.Items.IndexOf(selectedCells[i]);
                        viewModel.FrameDataList.RemoveAt(cellIndex);
                        
                    }

                    viewModel.ForceReloadFrameCountText();
                }
                e.Handled = true;
            }
        }

        private void NewFrameButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                return;
            }

            var viewModel = (P2M2ViewModel)DataContext;
            viewModel.FrameDataList.Add(new FrameDataViewModel());
            viewModel.ForceReloadFrameCountText();
        }

        private void AddRandomFrames_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Create a window for this method where you can input button weights (Odds for a button to be pressed per frame)
            if(DataContext == null)
            {
                return;
            }

            var viewModel = (P2M2ViewModel)DataContext;

            for(var i = 0; i < 100_000; i++)
            {
                viewModel.FrameDataList.Add(CreateRandomFrameData());
            }
            viewModel.ForceReloadFrameCountText();
        }

        private FrameDataViewModel CreateRandomFrameData()
        {
            var random = new Random();
            const int chance = 8;
            return new FrameDataViewModel
            {
                Buttons = new ButtonDataViewModel
                {
                    Circle = random.Next(0, chance) == 0,
                    Cross = random.Next(0, chance) == 0,
                    Triangle = random.Next(0, chance) == 0,
                    Square = random.Next(0, chance) == 0,
                    L1 = random.Next(0, chance) == 0,
                    L2 = random.Next(0, chance) == 0,
                    R1 = random.Next(0, chance) == 0,
                    R2 = random.Next(0, chance) == 0,
                    Down = random.Next(0, chance) == 0,
                    Up = random.Next(0, chance) == 0,
                    Right = random.Next(0, chance) == 0,
                    Left = random.Next(0, chance) == 0,
                    Select = random.Next(0, 100) == 0,
                    Start = random.Next(0, 100) == 0,
                    LeftAnalogStick = new AnalogStickViewModel
                    {
                        LeftRight = (byte)random.Next(0, 255),
                        UpDown = (byte)random.Next(0, 255)
                    },
                    RightAnalogStick = new AnalogStickViewModel
                    {
                        LeftRight = (byte)random.Next(0, 255),
                        UpDown = (byte)random.Next(0, 255)
                    }
                }
            };
        }
    }
}

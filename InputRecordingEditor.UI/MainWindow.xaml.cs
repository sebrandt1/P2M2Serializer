using InputRecordingEditor.UI.Converters;
using InputRecordingEditor.UI.FileManaging;
using InputRecordingEditor.UI.ViewModels;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using P2M2Serializer.Enums;
using P2M2Serializer.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private P2M2ViewModel backupViewModel;

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
                ((P2M2ViewModel)DataContext).ForceReloadIndexValues();
                ((P2M2ViewModel)DataContext).ForceReloadFrameCountText();
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

        private void FileMenuSplice_Click(object sender, RoutedEventArgs e)
        {
            var fileToInsert = p2m2InstanceManager.Open();
            var qaPrompt = new QuestionAnswerPrompt("Empty frames to add between recordings:", 0, "Ok");

            if(qaPrompt.ShowDialog() == true)
            {
                var framesToAdd = int.Parse(qaPrompt.AnswerTextBox.Text);
                if (framesToAdd > 0)
                {
                    for (var i = 0; i < framesToAdd; i++)
                    {
                        ((P2M2ViewModel)DataContext).FrameDataList.Add(new FrameDataViewModel());
                    }
                }
            }

            var splicedFrames = fileToInsert.FrameDataList;
            foreach(var frame in splicedFrames)
            {
                ((P2M2ViewModel)DataContext).FrameDataList.Add(frame);
            }

            ((P2M2ViewModel)DataContext).ForceReloadFrameCountText();
            ((P2M2ViewModel)DataContext).ForceReloadIndexValues();
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
                            var clone = new FrameDataViewModel
                            {
                                Buttons = convertedComboPreset.FrameDataList[j].Buttons.Clone()
                            };
                            viewModel.FrameDataList.Add(clone);
                        }
                    }
                    ((P2M2ViewModel)DataContext).ForceReloadIndexValues();
                    ((P2M2ViewModel)DataContext).ForceReloadFrameCountText();
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
                    try
                    {

                        var viewModel = (P2M2ViewModel)DataContext;
                        var itemsToRemove = selectedCells.Cast<FrameDataViewModel>().ToList();

                        foreach (var item in itemsToRemove)
                        {
                            viewModel.FrameDataList.Remove(item);
                        }
                        ((P2M2ViewModel)DataContext).ForceReloadIndexValues();
                        ((P2M2ViewModel)DataContext).ForceReloadFrameCountText();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                e.Handled = true;
            }
        }

        private void FrameDataGrid_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.M)
            {

                try
                {
                    var questionPrompt = new QuestionAnswerPrompt("Input index to move selected frames to:", 0, "Ok");
                    if (questionPrompt.ShowDialog() == true)
                    {
                        var moveTo = int.Parse(questionPrompt.AnswerTextBox.Text) - 1;
                        var viewModel = (P2M2ViewModel)DataContext;
                        var selectedCells = FrameDataGrid.SelectedItems.Cast<FrameDataViewModel>().ToList();
                        var isMovingDown = selectedCells.FirstOrDefault()?.Index < moveTo;
                        if(isMovingDown)
                        {
                            selectedCells = selectedCells.OrderBy(x => x.Index).ToList();
                        }
                        else
                        {
                            selectedCells = selectedCells.OrderByDescending(x => x.Index).ToList();
                        }
                        var currentIndex = 0;
                        foreach (var item in selectedCells)
                        {
                            viewModel.FrameDataList.Remove(item);
                            viewModel.FrameDataList.Insert(moveTo, item);
                            currentIndex++;
                        }
                        viewModel.ForceReloadFrameCountText();
                        viewModel.ForceReloadIndexValues();
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
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
            ((P2M2ViewModel)DataContext).ForceReloadIndexValues();
            ((P2M2ViewModel)DataContext).ForceReloadFrameCountText();
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
            ((P2M2ViewModel)DataContext).ForceReloadIndexValues();
            ((P2M2ViewModel)DataContext).ForceReloadFrameCountText();
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

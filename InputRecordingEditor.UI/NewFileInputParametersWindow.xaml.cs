using System.IO;
using System.Windows;
using System.Linq;

namespace InputRecordingEditor.UI
{
    /// <summary>
    /// Interaction logic for NewFileInputParametersWindow.xaml
    /// </summary>
    public partial class NewFileInputParametersWindow : Window
    {
        private const string VersionTooltip = "Version number of the pcsx2 emulator (e.g PCSX2-1.7.0)";
        private const string AuthorTooltip = "Name of the author of this input recording";
        private const string GameTooltip = "Name of the game (Not required for the recording to function)";
        private const string UndoCountTooltip = "The amount of input recording undos (When re-recording segments)";
        private const string SaveStateTooltip = "Should this input recording use the associated save state? Save state needs have the same name as the recording file name";
        private const string FileNameTooltip = "The name of the resulting file.";

        public NewFileInputParametersWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(UndoCountTextBox.Text, out var _))
            {
                MessageBox.Show($"Undo Count needs to be an integer between {int.MinValue} to {int.MaxValue}, actual value: {UndoCountTextBox.Text}");
                return;
            }
            if(string.IsNullOrWhiteSpace(FileNameTextBox.Text))
            {
                MessageBox.Show($"File name cannot be empty.");
                return;
            }
            var invalidFileNameChars = Path.GetInvalidFileNameChars()
                                        .Where(x => FileNameTextBox.Text.Contains(x));

            if (invalidFileNameChars.Any())
            {
                var invalidChars = string.Join(", ", invalidFileNameChars);
                MessageBox.Show($"File name was invalid, following characters may not be used in file names: {invalidChars}");
                return;
            }
            DialogResult = true;
        }
    }
}

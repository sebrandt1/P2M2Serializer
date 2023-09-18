using InputRecordingEditor.UI.Combos;
using InputRecordingEditor.UI.FileManaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.ViewModels
{
    public class ComboInsertionViewModel : INotifyPropertyChanged
    {
        public PresetContainer ComboPresets = ComboPresetSerializer.Load();

        public int RepeatCombo { get; set; }

        public List<string> Presets
        {
            get => ComboPresets.Presets.Select(x => x.Name).ToList();
        }

        public ComboPreset Result
        { 
            get; 
            set;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

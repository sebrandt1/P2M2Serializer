using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.ViewModels
{
    public class ComboPresetViewModel : INotifyPropertyChanged
    {
        public string? Name { get; set; }

        private ObservableCollection<FrameDataViewModel> _frameDataList = new ObservableCollection<FrameDataViewModel>();
        public ObservableCollection<FrameDataViewModel> FrameDataList
        {
            get { return _frameDataList; }
            set
            {
                if (_frameDataList != value)
                {
                    _frameDataList = value;
                    OnPropertyChanged(nameof(FrameDataList));
                    OnPropertyChanged(nameof(FrameCountText));
                }
            }
        }

        public void ForceReloadIndexValues()
        {
            for (var i = 0; i < FrameDataList.Count; i++)
            {
                FrameDataList[i].Index = i + 1;
            }
        }

        public string FrameCountText
        {
            get
            {
                return $"Frames: [{FrameDataList.Count}]";
            }
        }

        public void ForceReloadFrameCountText()
        {
            OnPropertyChanged(nameof(FrameCountText));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

namespace InputRecordingEditor.UI.ViewModels
{
    using P2M2Serializer.Structs;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;

    public class P2M2ViewModel : INotifyPropertyChanged
    {
        private string _author;
        private string _version;
        private string _game;
        private string _fileName;
        private bool _useSaveState;
        private int _undoCount;
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

        public string Author
        {
            get { return _author; }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(nameof(Version));
                }
            }
        }

        public string Game
        {
            get { return _game; }
            set
            {
                if (_game != value)
                {
                    _game = value;
                    OnPropertyChanged(nameof(Game));
                }
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }

        public bool UseSaveState
        {
            get { return _useSaveState; }
            set
            {
                if (_useSaveState != value)
                {
                    _useSaveState = value;
                    OnPropertyChanged(nameof(UseSaveState));
                }
            }
        }

        public int UndoCount
        {
            get { return _undoCount; }
            set
            {
                if (_undoCount != value)
                {
                    _undoCount = value;
                    OnPropertyChanged(nameof(UndoCount));
                }
            }
        }

        // Constructor
        public P2M2ViewModel()
        {
            // Initialize properties
            FrameDataList = new ObservableCollection<FrameDataViewModel>();
            Author = "";
            Version = "";
            Game = "";
            UseSaveState = false;
            UndoCount = 0;
        }

        public P2M2ViewModel(string author, string version, string game, bool useSaveState, int undoCount, ObservableCollection<FrameDataViewModel> frames)
        {
            FrameDataList = frames;
            Author = author;
            Version = version;
            Game = game;
            UseSaveState = useSaveState;
            UndoCount = undoCount;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

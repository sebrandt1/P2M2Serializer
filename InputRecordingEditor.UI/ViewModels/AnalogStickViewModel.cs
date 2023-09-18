using System.ComponentModel;

namespace InputRecordingEditor.UI.ViewModels
{
    public class AnalogStickViewModel : INotifyPropertyChanged
    {
        private byte _upDown;
        private byte _leftRight;

        public byte UpDown
        {
            get { return _upDown; }
            set
            {
                if (_upDown != value)
                {
                    _upDown = value;
                    OnPropertyChanged(nameof(UpDown));
                }
            }
        }

        public byte LeftRight
        {
            get { return _leftRight; }
            set
            {
                if (_leftRight != value)
                {
                    _leftRight = value;
                    OnPropertyChanged(nameof(LeftRight));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

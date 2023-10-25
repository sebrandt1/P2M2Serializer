using System.ComponentModel;

namespace InputRecordingEditor.UI.ViewModels
{
    public class FrameDataViewModel : INotifyPropertyChanged
    {
        private const byte NEUTRAL_JOYSTICK_STATE = 127;
        private ButtonDataViewModel _buttons;
        private int _index;

        public ButtonDataViewModel Buttons
        {
            get { return _buttons; }
            set
            {
                if (_buttons != value)
                {
                    _buttons = value;
                    OnPropertyChanged(nameof(Buttons));
                }
            }
        }

        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public FrameDataViewModel()
        {
            _buttons = new ButtonDataViewModel
            {
                LeftAnalogStick = new AnalogStickViewModel
                {
                    LeftRight = NEUTRAL_JOYSTICK_STATE,
                    UpDown = NEUTRAL_JOYSTICK_STATE
                },
                RightAnalogStick = new AnalogStickViewModel
                {
                    LeftRight = NEUTRAL_JOYSTICK_STATE,
                    UpDown = NEUTRAL_JOYSTICK_STATE
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using P2M2Serializer.Enums;
using System;
using System.ComponentModel;

namespace InputRecordingEditor.UI.ViewModels
{
    public class ButtonDataViewModel : INotifyPropertyChanged
    {
        private ButtonFlags _pressedButtonsFlag;
        private AnalogStickViewModel _rightAnalogStick;
        private AnalogStickViewModel _leftAnalogStick;
        private byte _right;
        private byte _left;
        private byte _up;
        private byte _down;
        private byte _triangle;
        private byte _circle;
        private byte _cross;
        private byte _square;
        private byte _l1;
        private byte _l2;
        private byte _r1;
        private byte _r2;

        public ButtonDataViewModel()
        {
            _rightAnalogStick = new AnalogStickViewModel();
            _leftAnalogStick = new AnalogStickViewModel();
        }

        public ButtonFlags PressedButtonsFlag
        {
            get { return _pressedButtonsFlag; }
            set
            {
                _pressedButtonsFlag = value;
                OnPropertyChanged(nameof(PressedButtonsFlag));
            }
        }

        public AnalogStickViewModel RightAnalogStick
        {
            get { return _rightAnalogStick; }
            set
            {
                if (_rightAnalogStick != value)
                {
                    _rightAnalogStick = value;
                    OnPropertyChanged(nameof(RightAnalogStick));
                }
            }
        }

        public AnalogStickViewModel LeftAnalogStick
        {
            get { return _leftAnalogStick; }
            set
            {
                if (_leftAnalogStick != value)
                {
                    _leftAnalogStick = value;
                    OnPropertyChanged(nameof(LeftAnalogStick));
                }
            }
        }

        public bool Right
        {
            get { return _right > 0; }
            set
            {
                SetValueOfButton(ref _right, value, ButtonFlags.Right);
                OnPropertyChanged(nameof(Right));
            }
        }

        public bool Left
        {
            get { return _left > 0; }
            set
            {
                SetValueOfButton(ref _left, value, ButtonFlags.Left);
                OnPropertyChanged(nameof(Left));
            }
        }

        public bool Up
        {
            get { return _up > 0; }
            set
            {
                SetValueOfButton(ref _up, value, ButtonFlags.Up);
                OnPropertyChanged(nameof(Up));
            }
        }

        public bool Down
        {
            get { return _down > 0; }
            set
            {
                SetValueOfButton(ref _down, value, ButtonFlags.Down);
                OnPropertyChanged(nameof(Down));
            }
        }

        public bool Triangle
        {
            get { return _triangle > 0; }
            set
            {
                SetValueOfButton(ref _triangle, value, ButtonFlags.Triangle);
                OnPropertyChanged(nameof(Triangle));
            }
        }

        public bool Circle
        {
            get { return _circle > 0; }
            set
            {
                SetValueOfButton(ref _circle, value, ButtonFlags.Circle);
                OnPropertyChanged(nameof(Circle));
            }
        }

        public bool Cross
        {
            get { return _cross > 0; }
            set
            {
                SetValueOfButton(ref _cross, value, ButtonFlags.Cross);
                OnPropertyChanged(nameof(Cross));
            }
        }

        public bool Square
        {
            get { return _square > 0; }
            set
            {
                SetValueOfButton(ref _square, value, ButtonFlags.Square);
                OnPropertyChanged(nameof(Square));
            }
        }

        public bool L1
        {
            get { return _l1 > 0; }
            set
            {
                SetValueOfButton(ref _l1, value, ButtonFlags.L1);
                OnPropertyChanged(nameof(L1));
            }
        }

        public bool L2
        {
            get { return _l2 > 0; }
            set
            {
                SetValueOfButton(ref _l2, value, ButtonFlags.L2);
                OnPropertyChanged(nameof(L2));
            }
        }

        public bool R1
        {
            get { return _r1 > 0; }
            set
            {
                SetValueOfButton(ref _r1, value, ButtonFlags.R1);
                OnPropertyChanged(nameof(R1));
            }
        }

        public bool R2
        {
            get { return _r2 > 0; }
            set
            {
                SetValueOfButton(ref _r2, value, ButtonFlags.R2);
                OnPropertyChanged(nameof(Right));
            }
        }

        public bool Select
        {
            get { return (PressedButtonsFlag & ButtonFlags.Select) == ButtonFlags.Select; }
            set
            {
                if (value)
                {
                    PressedButtonsFlag |= ButtonFlags.Select;
                }
                else
                {
                    PressedButtonsFlag &= ~ButtonFlags.Select;
                }
                OnPropertyChanged(nameof(Select));
            }
        }

        public bool Start
        {
            get { return (PressedButtonsFlag & ButtonFlags.Start) == ButtonFlags.Start; }
            set
            {
                if (value)
                {
                    PressedButtonsFlag |= ButtonFlags.Start;
                }
                else
                {
                    PressedButtonsFlag &= ~ButtonFlags.Start;
                }
                OnPropertyChanged(nameof(Start));
            }
        }

        public bool L3
        {
            get { return (PressedButtonsFlag & ButtonFlags.L3) == ButtonFlags.L3; }
            set
            {
                if (value)
                {
                    PressedButtonsFlag |= ButtonFlags.L3;
                }
                else
                {
                    PressedButtonsFlag &= ~ButtonFlags.L3;
                }
                OnPropertyChanged(nameof(L3));
            }
        }

        public bool R3
        {
            get { return (PressedButtonsFlag & ButtonFlags.R3) == ButtonFlags.R3; }
            set
            {
                if (value)
                {
                    PressedButtonsFlag |= ButtonFlags.R3;
                }
                else
                {
                    PressedButtonsFlag &= ~ButtonFlags.R3;
                }
                OnPropertyChanged(nameof(R3));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetValueOfButton(ref byte field, bool value, ButtonFlags flag)
        {
            if(value)
            {
                field = 255;
                PressedButtonsFlag |= flag;
            }
            else
            {
                field = 0;
                PressedButtonsFlag &= ~flag;
            }
        }
    }
}

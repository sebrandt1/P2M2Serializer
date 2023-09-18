using P2M2Serializer.Enums;
using System.Runtime.InteropServices;

namespace P2M2Serializer.Structs
{
    /// <summary>
    /// 18 bytes containing the data for the buttons being used (including analog sticks).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ButtonData
    {
        public ushort _pressedButtonsFlag;  // Offset 0 (2 bytes)
        public AnalogStick RightAnalogStick;// Offset 2 (2 bytes)
        public AnalogStick LeftAnalogStick; // Offset 4 (2 bytes)
        public byte Right;                  // Offset 6
        public byte Left;                   // Offset 7
        public byte Up;                     // Offset 8
        public byte Down;                   // Offset 9
        public byte Triangle;               // Offset 10
        public byte Circle;                 // Offset 11
        public byte Cross;                  // Offset 12
        public byte Square;                 // Offset 13
        public byte L1;                     // Offset 14
        public byte L2;                     // Offset 15
        public byte R1;                     // Offset 16
        public byte R2;                     // Offset 17

        public ButtonFlags PressedButtonsFlag
        {
            get
            {
                return (ButtonFlags)(~_pressedButtonsFlag & (ushort)~ButtonFlags.None);
            }
            set
            {
                _pressedButtonsFlag = (ushort)(~value & ~ButtonFlags.None);
            }
        }
    }

    /// <summary>
    /// 2 bytes containing the specified analog stick state.
    /// </summary>
    public struct AnalogStick
    {
        public byte UpDown;
        public byte LeftRight;
    }
}

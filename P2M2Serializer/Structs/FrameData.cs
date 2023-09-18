using System.Runtime.InteropServices;

namespace P2M2Serializer.Structs
{
    /// <summary>
    /// 36 bytes containing data on which buttons have been interacted with on the specific frame.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FrameData
    {
        /// <summary>
        /// Controller 1 buttons.
        /// </summary>
        public ButtonData Buttons;
        /// <summary>
        /// Controller 2 buttons.
        /// </summary>
        public ButtonData ButtonsClone;
    }
}

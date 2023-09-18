using System.Diagnostics;
using System.Runtime.InteropServices;

namespace P2M2Serializer.Structs
{
    /// <summary>
    /// Information for the input recording file.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InputRecordingInformation
    {
        public byte Header;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string Version;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string Author;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string Game;
        public int FrameCount;
        public int UndoCount;
        [MarshalAs(UnmanagedType.U1)] //1 byte enforcement. Booleans in structs are defaulted to 4 in C#.
        public bool UseSaveState;
    }
}

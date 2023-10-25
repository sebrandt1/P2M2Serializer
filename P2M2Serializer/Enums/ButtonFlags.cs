using System;

namespace P2M2Serializer.Enums
{
    /// <summary>
    /// These are not the REAL button values. They have been inverted from their original value to be easier to read.
    /// If you want to read or write them directly to/from the file with this enum they need to be inverted with the bitwise complement operator (~).
    /// </summary>
    [Flags]
    public enum ButtonFlags : ushort
    {
        None = 0,
        Select = 1,
        L3 = 2,
        R3 = 4,
        Start = 8,
        Up = 16,
        Right = 32,
        Down = 64,
        Left = 128,
        L2 = 256,
        R2 = 512,
        L1 = 1024,
        R1 = 2048,
        Triangle = 4096,
        Circle = 8192,
        Cross = 16384,
        Square = 32768,
        All = Select | L3 | R3 | Start | Up | Right | Down | Left | R1 | R2 | L1 | L2 | Triangle | Circle | Cross | Square
    }
}

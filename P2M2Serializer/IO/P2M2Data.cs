using P2M2Serializer.Structs;

namespace P2M2Serializer.IO
{
    /// <summary>
    /// Complete file data.
    /// </summary>
    public class P2M2Data
    {
        public InputRecordingInformation InputRecordingInformation { get; set; }
        public FrameData[] Frames { get; set; }
    }
}

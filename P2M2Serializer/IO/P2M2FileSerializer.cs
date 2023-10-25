using P2M2Serializer.Serializers;
using P2M2Serializer.Structs;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace P2M2Serializer.IO
{
    public class P2M2FileSerializer : IP2M2FileSerializer
    {
        private readonly IStructSerializer structConverter;

        public P2M2FileSerializer(IStructSerializer structConverter)
        {
            this.structConverter = structConverter;
        }

        public P2M2Data ReadFile(string path)
        {
            if (!IsP2M2File(path))
            {
                throw new ArgumentException("File extension was not the expected .p2m2 type.");
            }

            var fileData = File.ReadAllBytes(path);

            var inputRecordingStructSize = Marshal.SizeOf<InputRecordingInformation>();
            var inputRecording = structConverter.ToStruct<InputRecordingInformation>(fileData, 0, inputRecordingStructSize + 1);

            var frameCount = inputRecording.FrameCount;

            var frameSize = Marshal.SizeOf<FrameData>();
            var frames = new FrameData[frameCount];
            for (var i = 0; i < frames.Length; i++)
            {
                var nextFrameStartIndex = inputRecordingStructSize + i * frameSize;
                var nextFrameEndIndex = nextFrameStartIndex + frameSize;
                var frame = structConverter.ToStruct<FrameData>(fileData, nextFrameStartIndex, nextFrameEndIndex);
                frames[i] = frame;
            }

            return new P2M2Data
            {
                InputRecordingInformation = inputRecording,
                Frames = frames
            };
        }

        public void WriteFile(string path, P2M2Data p2m2)
        {
            //This is to ensure that the written file FrameCount matches the actual number of frames written
            var originalRecordingInfo = p2m2.InputRecordingInformation;

            var copyRecordingInfo = new InputRecordingInformation
            {
                Author = originalRecordingInfo.Author,
                Game = originalRecordingInfo.Game,
                Header = 1, //Need to make sure the header is always 1 regardless of what gets inputted
                UndoCount = originalRecordingInfo.UndoCount,
                UseSaveState = originalRecordingInfo.UseSaveState,
                Version = originalRecordingInfo.Version,
                FrameCount = p2m2.Frames.Length
            };

            //Write the copy instead of the original
            var inputRecordingBytesToWrite = structConverter.ToBytes(copyRecordingInfo);
            var frameBytesToWrite = structConverter.ArrayToBytes(p2m2.Frames);

            var output = new byte[inputRecordingBytesToWrite.Length + frameBytesToWrite.Length];

            var inputRecordingSize = Marshal.SizeOf<InputRecordingInformation>();
            for (var i = 0; i < output.Length; i++)
            {
                if (i < inputRecordingBytesToWrite.Length)
                {
                    output[i] = inputRecordingBytesToWrite[i];
                }
                else if (i < frameBytesToWrite.Length + inputRecordingBytesToWrite.Length)
                {
                    output[i] = frameBytesToWrite[i - inputRecordingSize];
                }
            }

            File.WriteAllBytes(path, output);
        }

        private static bool IsP2M2File(string path)
        {
            return Path.GetExtension(path) == ".p2m2";
        }
    }
}

namespace P2M2Serializer.Serializers
{
    public interface IStructSerializer
    {
        T ToStruct<T>(byte[] structBytes, int startIndex, int endIndex) where T : struct;
        byte[] ToBytes<T>(T structure) where T : struct;
        byte[] ArrayToBytes<T>(T[] items) where T : struct;
    }
}

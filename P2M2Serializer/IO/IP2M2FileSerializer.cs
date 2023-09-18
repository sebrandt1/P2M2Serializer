namespace P2M2Serializer.IO
{
    public interface IP2M2FileSerializer
    {
        P2M2Data ReadFile(string path);
        void WriteFile(string path, P2M2Data p2m2);
    }
}

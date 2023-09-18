using InputRecordingEditor.UI.ViewModels;
using P2M2Serializer.IO;

namespace InputRecordingEditor.UI.FileManaging
{
    public interface IP2M2InstanceManager
    {
        P2M2ViewModel New(string author, string game, string version, bool useSaveState, int undoCount, string fileName);
        P2M2ViewModel Open();
        void Save(string path, P2M2Data p2m2Data);
    }
}

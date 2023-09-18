using InputRecordingEditor.UI.Converters;
using InputRecordingEditor.UI.ViewModels;
using Microsoft.Win32;
using P2M2Serializer.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace InputRecordingEditor.UI.FileManaging
{
    public class P2M2InstanceManager : IP2M2InstanceManager
    {
        private readonly IP2M2FileSerializer serializer;

        public P2M2InstanceManager(IP2M2FileSerializer serializer)
        {
            this.serializer = serializer;
        }

        public P2M2ViewModel New(string author, string game, string version, bool useSaveState, int undoCount, string fileName)
        {
            return new P2M2ViewModel
            {
                Author = author,
                Game = game,
                Version = version,
                UseSaveState = useSaveState,
                UndoCount = undoCount,
                FrameDataList = new System.Collections.ObjectModel.ObservableCollection<FrameDataViewModel>(),
                FileName = fileName
            };
        }

        public P2M2ViewModel Open()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "P2M2 Files (*.p2m2)|*.p2m2";

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;

                var data = serializer.ReadFile(filePath);
                var recordingInfo = data.InputRecordingInformation;

                return new P2M2ViewModel
                {
                    Author = recordingInfo.Author,
                    Game = recordingInfo.Game,
                    Version = recordingInfo.Version,
                    UseSaveState = recordingInfo.UseSaveState,
                    UndoCount = recordingInfo.UndoCount,
                    FrameDataList = new ObservableCollection<FrameDataViewModel>(data.Frames.Select(x => ViewModelConverter.ToFrameDataViewModel(x))),
                    FileName = Path.GetFileName(openFileDialog.FileName).Replace(".p2m2", "")
                };
            }
            throw new System.Exception("Something went wrong when attempting to open file! ");
        }

        public void Save(string path, P2M2Data p2m2File)
        {
            var directory = Path.GetDirectoryName(path);
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            serializer.WriteFile(path, p2m2File);
        }
    }
}

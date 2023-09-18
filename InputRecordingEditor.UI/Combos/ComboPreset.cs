using InputRecordingEditor.UI.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace InputRecordingEditor.UI.Combos
{
    public class ComboPreset
    {
        public string? Name { get; set; }
        public List<FrameDataJson>? ComboFrames { get; set; }
    }
}

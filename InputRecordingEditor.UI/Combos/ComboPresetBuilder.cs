using InputRecordingEditor.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.Combos
{
    public class ComboPresetBuilder
    {
        public string? Name { get; set; }
        public List<FrameDataViewModel>? ComboFrames { get; set; }
    }
}

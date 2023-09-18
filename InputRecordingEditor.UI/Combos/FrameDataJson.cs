using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.Combos
{
    public class FrameDataJson
    {
        public int FrameNumber { get; set; }
        public ButtonDataJson? ControllerState { get; set; }
    }
}

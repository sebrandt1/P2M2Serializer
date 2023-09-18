using P2M2Serializer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.Combos
{
    public class ButtonDataJson
    {
        public AnalogStickJson? RightAnalogStick { get; set; }
        public AnalogStickJson? LeftAnalogStick { get; set; }
        public ButtonFlags PressedButtonFlags { get; set; }

    }
}

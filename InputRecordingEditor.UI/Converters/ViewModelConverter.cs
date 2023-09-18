using InputRecordingEditor.UI.ViewModels;
using P2M2Serializer.IO;
using P2M2Serializer.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.Converters
{
    public static class ViewModelConverter
    {
        public static FrameDataViewModel ToFrameDataViewModel(FrameData frameData)
        {
            return new FrameDataViewModel
            {
                Buttons = ToButtonsViewModel(frameData.Buttons)
            };
        }

        public static ButtonDataViewModel ToButtonsViewModel(ButtonData buttonData)
        {
            return new ButtonDataViewModel
            {
                PressedButtonsFlag = buttonData.PressedButtonsFlag,
                Circle = buttonData.Circle > 0,
                Cross = buttonData.Cross > 0,
                Square = buttonData.Square > 0,
                Triangle = buttonData.Triangle > 0,
                L1 = buttonData.L1 > 0,
                L2 = buttonData.L2 > 0,
                R1 = buttonData.R1 > 0,
                R2 = buttonData.R2 > 0,
                Down = buttonData.Down > 0,
                Left = buttonData.Left > 0,
                Right = buttonData.Right > 0,
                Up = buttonData.Up > 0,
                LeftAnalogStick = ToAnalogStickViewModel(buttonData.LeftAnalogStick),
                RightAnalogStick = ToAnalogStickViewModel(buttonData.RightAnalogStick),
            };
        }

        public static AnalogStickViewModel ToAnalogStickViewModel(AnalogStick analogStick)
        {
            return new AnalogStickViewModel
            {
                LeftRight = analogStick.LeftRight,
                UpDown = analogStick.UpDown,
            };
        }

        public static P2M2Data ToP2M2File(P2M2ViewModel viewModel)
        {
            return new P2M2Data
            {
                InputRecordingInformation = new InputRecordingInformation
                {
                    Author = viewModel.Author,
                    Game = viewModel.Game,
                    Header = 1,
                    UndoCount = viewModel.UndoCount,
                    UseSaveState = viewModel.UseSaveState,
                    Version = viewModel.Version,
                    FrameCount = viewModel.FrameDataList.Count
                },
                Frames = viewModel.FrameDataList.Select(x => ToFrameData(x)).ToArray()
            };
        }

        public static FrameData ToFrameData(FrameDataViewModel frameData)
        {
            return new FrameData
            {
                Buttons = ToButtonData(frameData.Buttons)
            };
        }

        public static ButtonData ToButtonData(ButtonDataViewModel buttonData)
        {
            return new ButtonData
            {
                PressedButtonsFlag = buttonData.PressedButtonsFlag,
                Circle = (byte)(buttonData.Circle ? 255 : 0),
                Cross = (byte)(buttonData.Cross ? 255 : 0),
                Square = (byte)(buttonData.Square ? 255 : 0),
                Triangle = (byte)(buttonData.Triangle ? 255 : 0),
                L1 = (byte)(buttonData.L1 ? 255 : 0),
                L2 = (byte)(buttonData.L2 ? 255 : 0),
                R1 = (byte)(buttonData.R1 ? 255 : 0),
                R2 = (byte)(buttonData.R2 ? 255 : 0),
                Down = (byte)(buttonData.Down ? 255 : 0),
                Left = (byte)(buttonData.Left ? 255 : 0),
                Right = (byte)(buttonData.Right ? 255 : 0),
                Up = (byte)(buttonData.Up ? 255 : 0),
                LeftAnalogStick = ToAnalogStick(buttonData.LeftAnalogStick),
                RightAnalogStick = ToAnalogStick(buttonData.RightAnalogStick),
            };
        }

        public static AnalogStick ToAnalogStick(AnalogStickViewModel analogStick)
        {
            return new AnalogStick
            {
                LeftRight = analogStick.LeftRight,
                UpDown = analogStick.UpDown,
            };
        }
    }
}

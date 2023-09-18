using InputRecordingEditor.UI.Combos;
using InputRecordingEditor.UI.ViewModels;
using P2M2Serializer.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputRecordingEditor.UI.Converters
{
    public static class ComboPresetConverter
    {
        private const byte NEUTRAL_JOYSTICK_STATE = 127;
        public static ComboPreset ToComboPreset(ComboPresetViewModel viewModel)
        {
            return new ComboPreset
            {
                Name = viewModel.Name,
                ComboFrames = viewModel.FrameDataList.Select((x, i) => ToJson(x, i)).ToList(),
            };
        }

        public static ComboPresetViewModel ToViewModel(ComboPreset preset)
        {
            return new ComboPresetViewModel
            {
                Name = preset.Name,
                FrameDataList = new ObservableCollection<FrameDataViewModel>(preset.ComboFrames.Select(x => ToViewModel(x)))
            };
        }

        private static FrameDataJson ToJson(FrameDataViewModel viewModel, int index)
        {
            var result = new List<FrameDataJson>();
            return new FrameDataJson
            {
                ControllerState = new ButtonDataJson
                {
                    LeftAnalogStick = new AnalogStickJson
                    {
                        UpDown = viewModel.Buttons.LeftAnalogStick.UpDown,
                        LeftRight = viewModel.Buttons.LeftAnalogStick.LeftRight,
                    },
                    RightAnalogStick = new AnalogStickJson
                    {
                        UpDown = viewModel.Buttons.RightAnalogStick.UpDown,
                        LeftRight = viewModel.Buttons.RightAnalogStick.LeftRight,
                    },
                    PressedButtonFlags = viewModel.Buttons.PressedButtonsFlag
                },
                FrameNumber = index + 1
            };
        }

        private static FrameDataViewModel ToViewModel(FrameDataJson json)
        {
            var result =  new FrameDataViewModel
            {
                Buttons = new ButtonDataViewModel
                {
                    LeftAnalogStick = new AnalogStickViewModel
                    {
                        LeftRight = json.ControllerState.LeftAnalogStick.LeftRight ?? NEUTRAL_JOYSTICK_STATE,
                        UpDown = json.ControllerState.LeftAnalogStick.UpDown ?? NEUTRAL_JOYSTICK_STATE,
                    },
                    RightAnalogStick = new AnalogStickViewModel
                    {
                        LeftRight = json.ControllerState.RightAnalogStick.LeftRight ?? NEUTRAL_JOYSTICK_STATE,
                        UpDown = json.ControllerState.RightAnalogStick.UpDown ?? NEUTRAL_JOYSTICK_STATE,
                    },
                }
            };

            var controllerState = json.ControllerState;

            if(controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Up))
            {
                result.Buttons.Up = true;
            }
            if(controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Left))
            {
                result.Buttons.Left = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Down))
            {
                result.Buttons.Down = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Right))
            {
                result.Buttons.Right = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Triangle))
            {
                result.Buttons.Triangle = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Circle))
            {
                result.Buttons.Circle = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Cross))
            {
                result.Buttons.Cross = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Square))
            {
                result.Buttons.Square = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.L1))
            {
                result.Buttons.L1 = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.L2))
            {
                result.Buttons.L2 = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.L3))
            {
                result.Buttons.L3 = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.R1))
            {
                result.Buttons.R1 = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.R2))
            {
                result.Buttons.R2 = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.R3))
            {
                result.Buttons.R3 = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Select))
            {
                result.Buttons.Select = true;
            }
            if (controllerState.PressedButtonFlags.HasFlag(ButtonFlags.Start))
            {
                result.Buttons.Start = true;
            }

            return result;
        }
    }
}

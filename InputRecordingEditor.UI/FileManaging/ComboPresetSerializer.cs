using InputRecordingEditor.UI.Combos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace InputRecordingEditor.UI.FileManaging
{
    public static class ComboPresetSerializer
    {
        private const string PresetFileName = "presets.json";
        public static void Save(ComboPreset preset)
        {
            var container = Load();
            var existingItem = container.Presets.FirstOrDefault(x => x.Name == preset.Name);
            if(existingItem != null)
            {
                var indexOf = container.Presets.IndexOf(existingItem);
                container.Presets[indexOf] = preset;
            }
            else
            { 
                container.Presets!.Add(preset);
            }

            var json = JsonSerializer.Serialize(container, new JsonSerializerOptions {  WriteIndented = true});

            if(ValidateFileExists())
            { 
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Combos", PresetFileName), json);
            }
        }

        public static PresetContainer Load()
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "Combos");
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var fullPath = Path.Combine(directory, PresetFileName);
            if(!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
            }
            var presetsRaw = File.ReadAllText(fullPath);

            PresetContainer presets = null;
            try
            { 
                presets = JsonSerializer.Deserialize<PresetContainer>(presetsRaw);
            }
            catch
            {
                presets = new PresetContainer() { Presets = new List<ComboPreset>() };
            }

            return presets;
        }

        private static bool ValidateFileExists()
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "Combos");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var fullPath = Path.Combine(directory, PresetFileName);
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
                
            }

            return File.Exists(fullPath);
        }
    }
}

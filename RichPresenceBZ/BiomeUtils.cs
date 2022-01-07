using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RichPresenceBZ
{
    public static class BiomeUtils
    {
        private static Dictionary<string, string> _biomes = new();
        private static List<string> _edgeCases = new();
        private static bool _initialized;
        private const string kBiomeFileName = "biomes.json";
        private const string kEdgeCasesFileName = "edgecases.json";

        public static string GetDisplayName(string biomeName, out bool isVanillaDisplayName)
        {
            Initialize();
            
            biomeName = biomeName.ToLower();

            isVanillaDisplayName = false;
            
            var edgeCase = EdgeCaseOrNull(biomeName);
            if (edgeCase is not null && _biomes.TryGetValue(edgeCase, out var value))
            {
                return value;
            }

            foreach (var kvp in _biomes)
            {
                if (biomeName.Contains(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            var result = Language.main.GetFormat($"Presence_{biomeName}");
            if (!string.IsNullOrEmpty(result))
            {
                isVanillaDisplayName = true;
                return result;
            }

            return biomeName;
        }

        public static string GetBiomeString(string biomeName)
        {
            biomeName = biomeName.ToLower();

            var edgeCase = EdgeCaseOrNull(biomeName);
            if (edgeCase is not null)
            {
                return edgeCase;
            }

            foreach (var kvp in _biomes)
            {
                if (biomeName.Contains(kvp.Key))
                {
                    return kvp.Key;
                }
            }
            
            return string.Empty;
        }

        private static string EdgeCaseOrNull(string biomeName) => _edgeCases.Contains(biomeName) ? biomeName : null;

        private static void Initialize()
        {
            if (_initialized)
                return;
            
            _biomes = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path.Combine(Main.AssetsFolder, kBiomeFileName)));
            _edgeCases = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Path.Combine(Main.AssetsFolder, kEdgeCasesFileName)));
            _initialized = true;
        }
    }
}
using System;
using SMLHelper.V2.Handlers;
#if SN1
using Oculus.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif

namespace CustomDataboxes.Converter
{
    class TechTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TechType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((TechType)value).AsString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var tt = (string)serializer.Deserialize(reader, typeof(string));

            return TechTypeExtensions.FromString(tt, out var techType, true) ?
                techType
                : TechTypeHandler.TryGetModdedTechType(tt, out techType) ?
                    techType
                    : throw new Exception($"Failed to parse {tt} into a TechType.");
        }
    }
}

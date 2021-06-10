using System;
using UnityEngine;
#if SN1
using Oculus.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif
namespace CustomDataboxes.Converter
{
    class Vector3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector3);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vector3 = (Vector3)value;

            serializer.Serialize(writer, new Vector3Json(vector3.x, vector3.y, vector3.z));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var v = (Vector3Json)serializer.Deserialize(reader, typeof(Vector3Json));

            return (object)new Vector3(v.x, v.y, v.z);
        }
    }

    struct Vector3Json
    {
        public float x;
        public float y;
        public float z;

        public Vector3Json(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}

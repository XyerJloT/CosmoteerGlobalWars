using Newtonsoft.Json;
using System;

namespace Assets.Scripts.Serialization
{
    public class SimpleJsonConverter<T, Adapter> : JsonConverter<T> where Adapter : ISerializeAdapter<T>, IDeserializeAdapter<T>, new()
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Adapter>(reader).GetOriginal();
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, AdapterFactory<T, Adapter>.Create(value));
        }
    }
}

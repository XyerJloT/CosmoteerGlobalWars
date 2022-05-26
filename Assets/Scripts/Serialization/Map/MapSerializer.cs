using Assets.Scripts.Map;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Serialization.Map
{
    public partial class MapSerializer
    {
        private JsonSerializerSettings _settings;

        public MapSerializer()
        {
            _settings = new JsonSerializerSettings()
            {
                Converters = new JsonConverter[]
                {
                    new SimpleJsonConverter<Vector2, Vector2Adapter>(),
                    new SimpleJsonConverter<Color, ColorAdapter>(),
                    new SimpleJsonConverter<SpaceMap, MapAdapter>()
                },
                Formatting = Formatting.Indented
            };
        }

        public SpaceMap Deserialize(string json) => JsonConvert.DeserializeObject<SpaceMap>(json, _settings);

        public string Serialize(SpaceMap map) => JsonConvert.SerializeObject(map, _settings);
    }
}

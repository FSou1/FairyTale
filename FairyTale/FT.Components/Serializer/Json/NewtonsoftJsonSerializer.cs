using Newtonsoft.Json;

namespace FT.Components.Serializer.Json {
    public class NewtonsoftJsonSerializer : ISerializer {
        public string Serialize(object obj) {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings() {
            Formatting = Formatting.Indented
        };
    }
}
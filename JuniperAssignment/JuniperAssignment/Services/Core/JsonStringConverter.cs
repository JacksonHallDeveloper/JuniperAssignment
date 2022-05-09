using Newtonsoft.Json;

namespace JuniperAssignment.Services.Core
{
    public class JsonStringConverter : IJsonConverter
    {
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
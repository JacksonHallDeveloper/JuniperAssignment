using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JuniperAssignment.Services.Core
{
    public class JsonRestService : IRestService
    {
        private readonly IJsonConverter _serializer;

        public JsonRestService(IJsonConverter serializer)
        {
            _serializer = serializer;
        }

        public HttpClient Client { get; set; }

        public async Task<T> GetAsync<T>(string resource)
        {
            var response = await Client.GetAsync(resource);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return _serializer.Deserialize<T>(json);
        }

        public async Task<T> PostAsync<T>(string resource, object payload)
        {
            var jsonPayload = _serializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(resource, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return _serializer.Deserialize<T>(json);
        }
    }
}
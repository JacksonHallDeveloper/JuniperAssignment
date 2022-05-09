using System.Net.Http;
using System.Threading.Tasks;

namespace JuniperAssignment.Services.Core
{
    public interface IRestService
    {
        HttpClient Client { get; set; }
        Task<T> GetAsync<T>(string resource);
        Task<T> PostAsync<T>(string resource, object payload);
    }
}
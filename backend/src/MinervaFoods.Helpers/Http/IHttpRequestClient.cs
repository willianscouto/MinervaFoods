using System.Net.Http.Headers;

namespace MinervaFoods.Helpers.Http
{
    public interface IHttpRequestClient
    {
        String BaseUrl { set; }
        T Get<T>(string endpoint, Dictionary<string, string>? headers = null);
        Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? headers = null);
        Task<T> PostAsync<T>(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null);
        Task<T> PutAsync<T>(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null);
        Task<T> DeleteAsync<T>(string endpoint, Dictionary<string, string>? headers = null);
        Task<T> DeleteAsync<T>(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null);
        Task DeleteAsync(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null);
      
    }
}

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MinervaFoods.Helpers.Http
{
    public class HttpRequestClient: IHttpRequestClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private string? _baseUrl;

        public HttpRequestClient(string? baseApiUrl = null)
        {

            _httpClient = new System.Net.Http.HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(1000000)
            };
            if (!string.IsNullOrEmpty(baseApiUrl))
            {
                _baseUrl = baseApiUrl.TrimEnd('/');
            }
        }

        public string? BaseUrl
        {
            get => _baseUrl;
            set => _baseUrl = string.IsNullOrEmpty(value) ? throw new InvalidOperationException("BaseUrl não pode ser nula.") : value.TrimEnd('/');
        }


        private HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, Dictionary<string, string>? headers = null)
        {
            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new InvalidOperationException("BaseUrl must be set before making requests.");
            }

            var request = new HttpRequestMessage(method, $"{_baseUrl}/{endpoint.TrimStart('/')}");

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
                    {
                        request.Headers.Authorization = AuthenticationHeaderValue.Parse(header.Value);
                    }
                    else
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            return request;
        }


        private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
        {
            
                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    throw new HttpRequestException(response.StatusCode,$"{errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseContent)
                  ?? throw new JsonException("Falha ao desserializar a resposta. Http");
          
        }

        private T SendRequest<T>(HttpRequestMessage request)
        {

            var response = _httpClient.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"{(int)response.StatusCode} # {errorContent}");
            }
            var responseContent = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(responseContent)
               ?? throw new JsonException("Falha ao desserializar a resposta.");

        }


        private async Task SendRequestAsync(HttpRequestMessage request)
        {

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException($"{(int)response.StatusCode} # {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();


        }

        public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Get, endpoint, headers);
            return await SendRequestAsync<T>(request);
        }

        public T Get<T>(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Get, endpoint, headers);
            return SendRequest<T>(request);
        }

        public async Task<T> PostAsync<T>(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Post, endpoint, headers);

            if (contentType == null)
                contentType = new MediaTypeHeaderValue("application/json");

            if (content != null) request.Content = CreateJsonContent(content);

            return await SendRequestAsync<T>(request);
        }

        public async Task<T> PutAsync<T>(string endpoint, object content, MediaTypeHeaderValue?contentType = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Put, endpoint, headers);

            if (contentType == null)
                contentType = new MediaTypeHeaderValue("application/json");


            if (content != null) request.Content = CreateJsonContent(content);

            return await SendRequestAsync<T>(request);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Delete, endpoint, headers);
            return await SendRequestAsync<T>(request);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Delete, endpoint, headers);

            if (contentType == null)
                contentType = new MediaTypeHeaderValue("application/json");

            if (content != null) request.Content = CreateJsonContent(content);

            return await SendRequestAsync<T>(request);
        }

        public async Task DeleteAsync(string endpoint, object content, MediaTypeHeaderValue? contentType = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Delete, endpoint, headers);

            if (contentType == null)
                contentType = new MediaTypeHeaderValue("application/json");

            if (content != null) request.Content = CreateJsonContent(content);

            await SendRequestAsync(request);
        }


     

        private StringContent CreateJsonContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }
    }
}

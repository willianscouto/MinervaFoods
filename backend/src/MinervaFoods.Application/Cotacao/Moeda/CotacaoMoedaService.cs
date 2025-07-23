using MinervaFoods.Helpers.Http;

namespace MinervaFoods.Application.Cotacao.Moeda
{
    public class CotacaoMoedaService : ICotacaoMoedaService
    {
        private readonly IHttpRequestClient _httpRequestClient;
        public CotacaoMoedaService(IHttpRequestClient httpRequestClient)
        {
            _httpRequestClient = httpRequestClient;
        }

        public async Task<ICollection<Domain.Entities.Cotacao>> GetAllCotacoes(CancellationToken cancellationToken)
        {
            _httpRequestClient.BaseUrl = "https://economia.awesomeapi.com.br/";

            var result = await _httpRequestClient.GetAsync<ICollection<Domain.Entities.Cotacao>>("json/last/:moedas");

            return result;
        }
    }
}

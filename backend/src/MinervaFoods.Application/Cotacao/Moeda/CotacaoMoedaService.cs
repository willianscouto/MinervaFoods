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

        //Atencao essa APi tem Limitacao de Cota
        //Erros na consulta 
        //{
        //{"success": false,
        //{"message": "{\"status\":429,\"code\":\"QuotaExceeded\",\"message\":\"Quota exceeded, see https://docs.awesomeapi.com.br/aviso-sobre-limites\"}\n",
        //{ "errors": []
        public async Task<Domain.Entities.Cotacao> GetMoedaCotacoes(string moeda,CancellationToken cancellationToken)
        {
            _httpRequestClient.BaseUrl = "https://economia.awesomeapi.com.br/";
            //var result = await _httpRequestClient.GetAsync<Dictionary<string, Domain.Entities.Cotacao>>($"json/last/{moeda}");
            //    return result.Values.First();
            var cotacaoFake = new Domain.Entities.Cotacao().GerarCotacaoFake(moeda);

           return await Task.FromResult(cotacaoFake);
     
        }






    }
}

namespace MinervaFoods.Application.Cotacao.Moeda
{
    public interface ICotacaoMoedaService
    {
        Task<Domain.Entities.Cotacao> GetMoedaCotacoes(string Moeda,CancellationToken cancellationToken);
    }
}

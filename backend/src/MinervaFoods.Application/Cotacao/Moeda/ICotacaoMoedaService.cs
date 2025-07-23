namespace MinervaFoods.Application.Cotacao.Moeda
{
    public interface ICotacaoMoedaService
    {
        Task<ICollection<Domain.Entities.Cotacao>> GetAllCotacoes(CancellationToken cancellationToken);
    }
}

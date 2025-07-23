using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Cotacao.Moeda;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.PedidosItens.PedidoItemCreate;
public class PedidoItemCreateHandler : IRequestHandler<PedidoItemCreateCommand, ICollection<PedidoItemResult>>
{
    private readonly ICarneRepository _carneRepository;
    private readonly ICotacaoMoedaService _cotacaoService;
    private readonly IMapper _mapper;

    public PedidoItemCreateHandler(
        ICarneRepository carneRepository,
        ICotacaoMoedaService cotacaoMoedaService,
        IMapper mapper)
    {
        _carneRepository = carneRepository;
        _cotacaoService = cotacaoMoedaService;
        _mapper = mapper;
    }

    public async Task<ICollection<PedidoItemResult>> Handle(PedidoItemCreateCommand command, CancellationToken cancellationToken)
    {
        await ValidateAsync(command, cancellationToken);

        var pedidosItens = _mapper.Map<ICollection<PedidoItem>>(command.Itens);

        var allCotacao = await _cotacaoService.GetAllCotacoes(cancellationToken);

        foreach (var item in pedidosItens)
        {
            var cotacao = allCotacao.FirstOrDefault(x => x.Code.Trim().ToUpper() == item.Moeda.ToString().ToUpper());
            if (cotacao == null)
                throw new KeyNotFoundException($"Cotação para moeda {item.Moeda} não encontrada.");

            item.AtualizarValorCotacao(cotacao.Bid);
        }

        return _mapper.Map<ICollection<PedidoItemResult>>(pedidosItens);
    }

    private async Task ValidateAsync(PedidoItemCreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new PedidoItemCreateValidator();

        foreach (var com in command.Itens)
        {
            var validationResult = await validator.ValidateAsync(com, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        var carnes = await _carneRepository.GetByIdAsync(command.Itens.Select(x => x.CarneId), cancellationToken);
        var carneIds = carnes.Select(c => c.Id).ToHashSet();

        var invalidItems = command.Itens.Where(i => !carneIds.Contains(i.CarneId));
        if (invalidItems.Any())
            throw new KeyNotFoundException("One or more products specified in the sale items were not found.");
    }
}

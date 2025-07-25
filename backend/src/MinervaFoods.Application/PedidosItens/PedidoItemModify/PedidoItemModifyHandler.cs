using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Cotacao.Moeda;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.PedidosItens.PedidoItemModify;
public class PedidoItemModifyHandler : IRequestHandler<PedidoItemModifyCommand, ICollection<PedidoItemResult>>
{
    private readonly IPedidoItemRepository _repository;
    private readonly ICarneRepository _carneRepository;
    private readonly ICotacaoMoedaService _cotacaoService;
    private readonly IMapper _mapper;


    public PedidoItemModifyHandler(
        IPedidoItemRepository repository,
        ICotacaoMoedaService cotacaoMoedaService,
        ICarneRepository carneRepository,
        IMapper mapper)
    {
        _repository = repository;
        _cotacaoService = cotacaoMoedaService;
        _carneRepository = carneRepository;
        _mapper = mapper;
        
    }

    public async Task<ICollection<PedidoItemResult>> Handle(PedidoItemModifyCommand command, CancellationToken cancellationToken)
    {

        var pedidosItens = await ValidateAsync(command, cancellationToken);

        foreach (var item in pedidosItens)
        {
            if (item.Moeda == Domain.Enums.PedidoItemEnum.Moeda.BRL) { item.AtualizarValorCotacao(1); continue; }
            var cotacaoMoeda = await _cotacaoService.GetMoedaCotacoes(item.Moeda.ToString(), cancellationToken);
            var itensCommand = command.Itens.Where(x => x.Id == item.Id).FirstOrDefault();
            if (cotacaoMoeda == null)
                throw new KeyNotFoundException($"Cotação para moeda {item.Moeda} não encontrada.");

            item.Atualizar(itensCommand!.Quantidade,itensCommand.PrecoUnitario, cotacaoMoeda.Bid);
        }

        pedidosItens = await _repository.UpdateAsync(pedidosItens, cancellationToken);
        return _mapper.Map<ICollection<PedidoItemResult>>(pedidosItens);
    }

    private async Task<IEnumerable<PedidoItem>> ValidateAsync(PedidoItemModifyCommand command, CancellationToken cancellationToken)
    {
        var validator = new PedidoItemModifyValidator();

        foreach (var com in command.Itens)
        {
            var validationResult = await validator.ValidateAsync(com, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        var carnes = await _carneRepository.GetByIdAsync(command.Itens.Select(x => x.CarneId), cancellationToken);
        var carneIds = carnes.Select(c => c.Id).ToHashSet();

        var invalidCarneItems = command.Itens.Where(i => carneIds.Contains(i.CarneId));
        if (!invalidCarneItems.Any())
            throw new KeyNotFoundException("One or more products specified in the sale items were not found.");


        var pedidosItens = await _repository.GetByIdAsync(command.Itens.Select(c => c.Id), cancellationToken);
        var pedidosItensIds = pedidosItens.Select(c => c.Id).ToHashSet();

        var invalidItems = command.Itens.Where(i => pedidosItensIds.Contains(i.Id));
        if (!invalidItems.Any())
            throw new KeyNotFoundException("One or more products specified in the sale items were not found.");

        return pedidosItens;
    }
}

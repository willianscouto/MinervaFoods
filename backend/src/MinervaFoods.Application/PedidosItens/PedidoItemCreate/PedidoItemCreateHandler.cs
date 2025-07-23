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
    private readonly IPedidoItemRepository _repository;
    private readonly ICotacaoMoedaService _cotacaoService;
    private readonly IMapper _mapper;

    public PedidoItemCreateHandler(
        ICarneRepository carneRepository,
        ICotacaoMoedaService cotacaoMoedaService,
        IPedidoItemRepository repository,
        IMapper mapper)
    {
        _carneRepository = carneRepository;
        _cotacaoService = cotacaoMoedaService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<PedidoItemResult>> Handle(PedidoItemCreateCommand command, CancellationToken cancellationToken)
    {

        await ValidateAsync(command, cancellationToken);


        var pedidosItens = _mapper.Map<IEnumerable<PedidoItem>>(command.Itens);


        foreach (var item in pedidosItens)
        {
            var cotacaoMoeda = await _cotacaoService.GetMoedaCotacoes(item.Moeda.ToString(), cancellationToken);

            if (cotacaoMoeda == null)
                throw new KeyNotFoundException($"Cotação para moeda {item.Moeda} não encontrada.");

            item.AtualizarValorCotacao(cotacaoMoeda.Bid);
        }

        if (command.PedidoId!= Guid.Empty)
        {
            pedidosItens.ToList().ForEach(p => p.PedidoId = command.PedidoId);
            pedidosItens = await _repository.CreateAsync(pedidosItens, cancellationToken);

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
            throw new KeyNotFoundException("Um ou mais produtos informados nos itens não foram encontrados.");


        var gruposInvalidos = command.Itens
         .GroupBy(i => new { i.CarneId, i.Moeda })
         .Where(g => g.Select(x => x.PrecoUnitario).Distinct().Count() > 1)
         .ToList();

        if (gruposInvalidos.Any())
        {
            var detalhes = string.Join(", ", gruposInvalidos.Select(g => $"CarneId: {g.Key.CarneId}, Moeda: {g.Key.Moeda}"));
            throw new InvalidOperationException($"Existem múltiplos preços para os mesmos CarneId e Moeda: {detalhes}. Agrupe antes de enviar.");
        }

    }
}

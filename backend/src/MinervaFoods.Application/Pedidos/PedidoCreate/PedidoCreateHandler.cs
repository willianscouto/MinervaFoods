using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Pedidos.PedidoCreate
{
    /// <summary>
    /// Handler responsável por processar o comando de criação de pedido.
    /// </summary>
    public class PedidoCreateHandler : IRequestHandler<PedidoCreateCommand, PedidoCreateResult>
    {
        private readonly IPedidoRepository _repository;
        private readonly ICarneRepository _carneRepository;
        private readonly ICompradorRepository _compradorRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoCreateHandler"/>.
        /// </summary>
        public PedidoCreateHandler(
            IPedidoRepository repository,
            ICarneRepository carneRepository,
            ICompradorRepository compradorRepository,
            IMediator mediator,
            IMapper mapper)
        {
            _repository = repository;
            _carneRepository = carneRepository;
            _compradorRepository = compradorRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula o comando <see cref="PedidoCreateCommand"/>.
        /// Valida o comando, cria o pedido e retorna o resultado.
        /// </summary>
        public async Task<PedidoCreateResult> Handle(PedidoCreateCommand command, CancellationToken cancellationToken)
        {
            await ValidateCommandAsync(command, cancellationToken);

            var pedidoItens = await GetPedidoItemsAsync(command.PedidoItem, cancellationToken);

            var proximoNumeroPedido = await _repository.GetProximoNumeroPedidoAsync(cancellationToken);

            var pedido = new Domain.Entities.Pedido(command.CompradorId, proximoNumeroPedido, command.DataPedido);
            
            pedido.AdicionarItem(pedidoItens);
            pedido.AdicionarNumeroPedido(proximoNumeroPedido);

            pedido = await _repository.CreateAsync(pedido, cancellationToken);

            return _mapper.Map<PedidoCreateResult>(pedido);
        }

        /// <summary>
        /// Valida o comando de criação de pedido e suas dependências (comprador e carnes).
        /// </summary>
        private async Task ValidateCommandAsync(PedidoCreateCommand command, CancellationToken cancellationToken)
        {
            var validator = new PedidoCreateValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comprador = await _compradorRepository.GetByIdAsync(command.CompradorId, cancellationToken);
            if (comprador == null)
                throw new KeyNotFoundException($"Comprador com ID {command.CompradorId} não encontrado.");

          
            var carnesIds = command.PedidoItem.Select(i => i.CarneId).Distinct();
            var carnes = await _carneRepository.GetByIdAsync(carnesIds, cancellationToken);
            var carnesEncontradas = carnes.Select(c => c.Id).ToHashSet();

            var carnesNaoEncontradas = carnesIds.Where(id => !carnesEncontradas.Contains(id));
            if (carnesNaoEncontradas.Any())
                throw new KeyNotFoundException($"Carnes com os seguintes IDs não foram encontradas: {string.Join(", ", carnesNaoEncontradas)}");
        }

        /// <summary>
        /// Dispara o comando de criação dos itens de pedido e retorna a lista de entidades mapeadas.
        /// </summary>
        private async Task<ICollection<PedidoItem>> GetPedidoItemsAsync(ICollection<PedidoItemCreateItem> pedidoItems, CancellationToken cancellationToken)
        {
            var command = new PedidoItemCreateCommand
            {
                Itens = pedidoItems.ToList()
            };

            var result = await _mediator.Send(command, cancellationToken);
            return _mapper.Map<ICollection<PedidoItem>>(result);
        }
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Pedidos.Common;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Application.PedidosItens.PedidoItemDelete;
using MinervaFoods.Application.PedidosItens.PedidoItemModify;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Pedidos.PedidoModify
{
    /// <summary>
    /// Handler responsável por processar requisições de modificação de pedidos (<see cref="PedidoModifyCommand"/>).
    /// </summary>
    public class PedidoModifyHandler : IRequestHandler<PedidoModifyCommand, PedidoResult>
    {
        private readonly IPedidoRepository _repository;
        private readonly ICompradorRepository _compradorRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoModifyHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de pedidos.</param>
        /// <param name="compradorRepository">Repositório de compradores.</param>
        /// <param name="mediator">Instância do Mediator para envio de comandos internos.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public PedidoModifyHandler(
            ICompradorRepository compradorRepository,
            IPedidoRepository repository,
            IMediator mediator,
            IMapper mapper)
        {
            _compradorRepository = compradorRepository;
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Manipula a requisição de modificação de pedido (<see cref="PedidoModifyCommand"/>).
        /// </summary>
        /// <param name="command">Comando contendo os dados de modificação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da modificação, incluindo os dados atualizados.</returns>
        public async Task<PedidoResult> Handle(PedidoModifyCommand command, CancellationToken cancellationToken)
        {
            var pedido = await ValidateAsync(command, cancellationToken);
            List<PedidoItem> allPedidosItens = new List<PedidoItem>();

            var itemCarnesToKeep = command.PedidoItem.Where(x => x.CarneId != Guid.Empty).Select(x => x.CarneId);
            var itemsToDelete = pedido.PedidoItem.Where(item => !itemCarnesToKeep.Contains(item.CarneId)).ToList();

            if (itemsToDelete.Count != 0)
            {
                var commandDelete = new PedidoItemDeleteCommand(itemsToDelete.Select(x => x.Id));
                await _mediator.Send(commandDelete, cancellationToken); // Corrigido: estava chamando o command errado
            }

            var itemsToUpdate = pedido.PedidoItem
                .Where(existing => command.PedidoItem.Any(c => c.CarneId == existing.CarneId && c.Quantidade != existing.Quantidade))
                .Select(c => c.Id);

            if (itemsToUpdate.Any())
            {
                var itens = pedido.PedidoItem.Where(item => itemsToUpdate.Contains(item.Id));
                var itensModify = _mapper.Map<IEnumerable<PedidoItemModifyItem>>(itens);
                var commandUpdate = new PedidoItemModifyCommand { Itens = itensModify.ToList() };
                var resultModify = await _mediator.Send(commandUpdate, cancellationToken);
                allPedidosItens.AddRange(_mapper.Map<IEnumerable<PedidoItem>>(resultModify));
            }

            var itemsToAdd = command.PedidoItem
                .Where(existing => !pedido.PedidoItem.Any(c => c.CarneId == existing.CarneId))
                .Select(c => c.CarneId);

            if (itemsToAdd.Any())
            {
                var pedidoCommandItemAdd = command.PedidoItem.Where(item => itemsToAdd.Contains(item.CarneId));
                var itensCreate = _mapper.Map<IEnumerable<PedidoItemCreateItem>>(pedidoCommandItemAdd);
                var commandCreate = new PedidoItemCreateCommand { Itens = itensCreate.ToList() };
                var resultAdd = await _mediator.Send(commandCreate, cancellationToken);
                allPedidosItens.AddRange(_mapper.Map<IEnumerable<PedidoItem>>(resultAdd));
            }

            pedido.ClearItens();
            pedido.AdicionarItem(allPedidosItens);
            await _repository.UpdateAsync(pedido);

            return _mapper.Map<PedidoResult>(pedido);
        }

        /// <summary>
        /// Valida o comando <see cref="PedidoModifyCommand"/> quanto às regras de negócio e integridade dos dados.
        /// </summary>
        /// <param name="command">Comando a ser validado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>A entidade <see cref="Pedido"/> a ser modificada, se válida.</returns>
        /// <exception cref="ValidationException">Lançada quando as regras de validação falham.</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando o pedido ou o comprador não é encontrado.</exception>
        private async Task<Pedido> ValidateAsync(PedidoModifyCommand command, CancellationToken cancellationToken)
        {
            var validator = new PedidoModifyValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var pedido = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com ID {command.Id} não foi encontrado.");

            var compradorExists = await _compradorRepository.GetByIdAsync(command.CompradorId, cancellationToken);
            if (compradorExists == null)
                throw new KeyNotFoundException($"Comprador com ID {command.CompradorId} não foi encontrado.");

            return pedido;
        }
    }
}

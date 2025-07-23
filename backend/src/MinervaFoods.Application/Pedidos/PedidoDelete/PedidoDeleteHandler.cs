using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.PedidosItens.PedidoItemDelete;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Pedidos.PedidoDelete
{
    /// <summary>
    /// Handler responsável por processar o comando de deleção de um pedido.
    /// </summary>
    public class PedidoDeleteHandler : IRequestHandler<PedidoDeleteCommand, PedidoDeleteResult>
    {
        private readonly IPedidoRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoDeleteHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de pedidos.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public PedidoDeleteHandler(IPedidoRepository repository, 
        IMediator mediator,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Processa a lógica para deletar um pedido.
        /// </summary>
        /// <param name="command">Comando contendo o ID do pedido a ser deletado.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Resultado indicando se a exclusão foi bem-sucedida.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando é inválido.</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando o pedido não é encontrado.</exception>
        public async Task<PedidoDeleteResult> Handle(PedidoDeleteCommand command, CancellationToken cancellationToken)
        {
            var validator = new PedidoDeleteValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var pedidoToDelete = await _repository.GetByIdAsync(command.Id, cancellationToken, "PedidoItem");
            if (pedidoToDelete == null)
                throw new KeyNotFoundException($"Pedido com ID {command.Id} não encontrado.");

            var deleteCommandItem = new PedidoItemDeleteCommand(pedidoToDelete.PedidoItem.Select(x => x.Id));
            await _mediator.Send(deleteCommandItem, cancellationToken);

            var success = await _repository.DeleteAsync(pedidoToDelete, cancellationToken);

            return new PedidoDeleteResult
            {
                Success = success
            };
            
        }
    }
}

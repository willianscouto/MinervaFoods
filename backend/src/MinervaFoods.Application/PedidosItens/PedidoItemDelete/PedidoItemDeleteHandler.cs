using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.PedidosItens.PedidoItemDelete
{
    /// <summary>
    /// Handler responsável por processar o comando de deleção de um pedido.
    /// </summary>
    public class PedidoItemDeleteHandler : IRequestHandler<PedidoItemDeleteCommand, PedidoItemDeleteResult>
    {
        private readonly IPedidoItemRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoDeleteHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de pedidos.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public PedidoItemDeleteHandler(IPedidoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Processa a lógica para deletar um pedido.
        /// </summary>
        /// <param name="command">Comando contendo o ID do pedido a ser deletado.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Resultado indicando se a exclusão foi bem-sucedida.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando é inválido.</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando o pedido não é encontrado.</exception>
        public async Task<PedidoItemDeleteResult> Handle(PedidoItemDeleteCommand command, CancellationToken cancellationToken)
        {
            var validator = new PedidoItemDeleteValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

           
            var itens = await _repository.GetByIdAsync(command.Ids, cancellationToken);
            var carnesEncontradas = itens.Select(c => c.Id).ToHashSet();

            var carnesNaoEncontradas = command.Ids.Where(id => !carnesEncontradas.Contains(id));
            if (carnesNaoEncontradas.Any())
                throw new KeyNotFoundException($"Carnes com os seguintes IDs não foram encontradas: {string.Join(", ", carnesNaoEncontradas)}");


            var success = await _repository.DeleteAsync(itens, cancellationToken);
          

            return _mapper.Map<PedidoItemDeleteResult>(success);
        }
    }
}

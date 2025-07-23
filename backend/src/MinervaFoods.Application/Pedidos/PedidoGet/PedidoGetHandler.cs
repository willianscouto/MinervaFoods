using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Pedidos.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Pedidos.PedidoGet
{
    /// <summary>
    /// Handler para o comando <see cref="PedidoGetCommand"/>.
    /// </summary>
    public class PedidoGetHandler : IRequestHandler<PedidoGetCommand, PedidoResult>
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="PedidoGetHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de pedidos.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public PedidoGetHandler(
            IPedidoRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa o comando para obter um pedido pelo seu identificador.
        /// </summary>
        /// <param name="command">Comando contendo o Id do pedido.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação.</param>
        /// <returns>Retorna um objeto <see cref="PedidoResult"/> com os dados do pedido.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando não é válido.</exception>
        public async Task<PedidoResult> Handle(PedidoGetCommand command, CancellationToken cancellationToken)
        {
            var validator = new PedidoGetValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var entity = await _repository.GetByIdAsync(command.Id, cancellationToken,"Comprador", "PedidoItem", "PedidoItem.Carnes");
            return _mapper.Map<PedidoResult>(entity);
        }
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Carnes.CarneDelete;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Compradores.CompradorDelete
{
    /// <summary>
    /// Handler responsável por processar o comando de deleção de um comprador.
    /// </summary>
    public class CompradorDeleteHandler : IRequestHandler<CompradorDeleteCommand, CompradorDeleteResult>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICompradorRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CompradorDeleteHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de comprador.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public CompradorDeleteHandler(ICompradorRepository repository, 
        IPedidoRepository pedidoRepository,
        IMapper mapper)
        {
            _repository = repository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Processa a lógica para deletar um comprador.
        /// </summary>
        /// <param name="command">Comando contendo o ID do comprador a ser deletado.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Resultado indicando se a exclusão foi bem-sucedida.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando é inválido.</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando o comprador não é encontrado.</exception>
        public async Task<CompradorDeleteResult> Handle(CompradorDeleteCommand command, CancellationToken cancellationToken)
        {
            var validator = new CompradorDeleteValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var compradorToDelete = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (compradorToDelete == null)
                throw new KeyNotFoundException($"Comprador com ID {command.Id} não encontrado.");

            var hasPedidosAbertos = await _pedidoRepository.FindAsync(p => p.CompradorId == command.Id && p.StatusPedido== Domain.Enums.PedidoEnum.Status.Aberto);

            if (hasPedidosAbertos.Any())
                throw new InvalidOperationException("Não é possível excluir o comprador pois existem pedidos em aberto.");



            var success = await _repository.DeleteAsync(compradorToDelete, cancellationToken);

            return new CompradorDeleteResult
            {
                Success = success
            };
         
        }
    }
}

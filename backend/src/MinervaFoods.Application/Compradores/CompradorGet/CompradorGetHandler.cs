using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Compradores.CompradorGet
{
    /// <summary>
    /// Handler para o comando <see cref="CompradorGetCommand"/>.
    /// </summary>
    public class CompradorGetHandler : IRequestHandler<CompradorGetCommand, CompradorResult>
    {
        private readonly ICompradorRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler <see cref="CompradorGetHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de compradores.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public CompradorGetHandler(
            ICompradorRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa o comando para obter um comprador pelo seu identificador.
        /// </summary>
        /// <param name="command">Comando contendo o Id do comprador.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação.</param>
        /// <returns>Retorna um objeto <see cref="CompradorResult"/> com os dados do comprador.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando não é válido.</exception>
        public async Task<CompradorResult> Handle(CompradorGetCommand command, CancellationToken cancellationToken)
        {
            var validator = new CompradorGetValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);

            return _mapper.Map<CompradorResult>(entity);
        }
    }
}

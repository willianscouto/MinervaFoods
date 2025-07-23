using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Estados.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Estados.EstadoGetByPais
{
    /// <summary>
    /// Handler para o comando <see cref="EstadoGetByPaisCommand"/>.
    /// </summary>
    public class EstadoGetByPaisHandler : IRequestHandler<EstadoGetByPaisCommand, IEnumerable<EstadoResult>>
    {
        private readonly IEstadoRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler <see cref="EstadoGetByPaisHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de estados.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public EstadoGetByPaisHandler(
            IEstadoRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa o comando para obter os estados vinculados a um país.
        /// </summary>
        /// <param name="command">Comando contendo o Id do país.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação.</param>
        /// <returns>Retorna uma lista de <see cref="EstadoResult"/> associados ao país informado.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando não é válido.</exception>
        public async Task<IEnumerable<EstadoResult>> Handle(EstadoGetByPaisCommand command, CancellationToken cancellationToken)
        {
            var validator = new EstadoGetByPaisValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var estados = await _repository.FindAsync(f => f.PaisId == command.PaisId, cancellationToken);

            return _mapper.Map<IEnumerable<EstadoResult>>(estados);
        }
    }
}

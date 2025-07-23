using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Application.Estados.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Estados.EstadoGet
{
    /// <summary>
    /// Handler para o comando <see cref="CarneGetCommand"/>.
    /// </summary>
    public class EstadoGetHandler : IRequestHandler<EstadoGetCommand, EstadoResult>
    {
        private readonly ICarneRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler <see cref="CarneGetHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de carnes.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public EstadoGetHandler(
            ICarneRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa o comando para obter uma carne pelo seu identificador.
        /// </summary>
        /// <param name="command">Comando contendo o Id da carne.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação.</param>
        /// <returns>Retorna um objeto <see cref="CarneResult"/> com os dados da carne.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando não é válido.</exception>
        public async Task<EstadoResult> Handle(EstadoGetCommand command, CancellationToken cancellationToken)
        {
            var validator = new EstadoGetValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);

            return _mapper.Map<EstadoResult>(entity);
        }
    }
}

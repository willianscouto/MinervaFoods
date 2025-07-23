using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Paises.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Paises.PaisGet
{
    /// <summary>
    /// Handler para o comando <see cref="PaisGetCommand"/>.
    /// </summary>
    public class PaisGetHandler : IRequestHandler<PaisGetCommand, PaisResult>
    {
        private readonly IPaisRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler <see cref="PaisGetHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de países.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public PaisGetHandler(
            IPaisRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa o comando para obter um país pelo seu identificador.
        /// </summary>
        /// <param name="command">Comando contendo o Id do país.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação.</param>
        /// <returns>Retorna um objeto <see cref="PaisResult"/> com os dados do país.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando não é válido.</exception>
        public async Task<PaisResult> Handle(PaisGetCommand command, CancellationToken cancellationToken)
        {
            var validator = new PaisGetValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);

            return _mapper.Map<PaisResult>(entity);
        }
    }
}

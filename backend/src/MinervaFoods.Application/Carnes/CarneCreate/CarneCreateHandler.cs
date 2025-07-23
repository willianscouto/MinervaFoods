using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Carnes.CarneCreate
{
    /// <summary>
    /// Handler responsável por processar os comandos de criação de carne.
    /// </summary>
    public class CarneCreateHandler : IRequestHandler<CarneCreateCommand, CarneCreateResult>
    {
        private readonly ICarneRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CarneCreateHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de carnes.</param>
        /// <param name="mapper">Instância do AutoMapper para mapear objetos.</param>
        public CarneCreateHandler(
            ICarneRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula o comando <see cref="CarneCreateCommand"/>.
        /// Valida o comando, cria a carne e retorna o resultado.
        /// </summary>
        /// <param name="command">Comando com os dados da carne.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da criação contendo os dados da carne criada.</returns>
        public async System.Threading.Tasks.Task<CarneCreateResult> Handle(CarneCreateCommand command, CancellationToken cancellationToken)
        {
            await ValidateAsync(command, cancellationToken);

            var carne = _mapper.Map<Carne>(command);
            var entity = await _repository.CreateAsync(carne, cancellationToken);

            return _mapper.Map<CarneCreateResult>(entity);
        }

        /// <summary>
        /// Valida o comando <see cref="CarneCreateCommand"/> conforme as regras de negócio.
        /// </summary>
        /// <param name="command">Comando a ser validado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        private async System.Threading.Tasks.Task ValidateAsync(CarneCreateCommand command, CancellationToken cancellationToken)
        {
            var validator = new CarneCreateValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingEAN = await _repository.FindAsync(c => c.Ean == command.Ean, cancellationToken);
            if (existingEAN.Any())
                throw new InvalidOperationException($"Produto com EAN {command.Ean} já existe.");

            var existingProduct = await _repository.FindAsync(c => c.Nome == command.Nome, cancellationToken);
            if (existingProduct.Any())
                throw new InvalidOperationException($"Produto com nome {command.Nome} já existe.");
        }
    }
}

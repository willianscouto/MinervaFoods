using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Carnes.CarneDelete
{
    /// <summary>
    /// Handler responsável por processar o comando de deleção de uma carne.
    /// </summary>
    public class CarneDeleteHandler : IRequestHandler<CarneDeleteCommand, CarneDeleteResult>
    {
        private readonly ICarneRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CarneDeleteHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de carne.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public CarneDeleteHandler(ICarneRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Processa a lógica para deletar uma carne.
        /// </summary>
        /// <param name="command">Comando contendo o ID da carne a ser deletada.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Resultado indicando se a exclusão foi bem-sucedida.</returns>
        /// <exception cref="ValidationException">Lançada quando o comando é inválido.</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando a carne não é encontrada.</exception>
        public async Task<CarneDeleteResult> Handle(CarneDeleteCommand command, CancellationToken cancellationToken)
        {
            var validator = new CarneDeleteValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var carneToDelete = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (carneToDelete == null)
                throw new KeyNotFoundException($"Carne com ID {command.Id} não encontrada.");

            var success = await _repository.DeleteAsync(carneToDelete, cancellationToken);

            return new CarneDeleteResult
            {
                Success = success
            };
        }
    }
}

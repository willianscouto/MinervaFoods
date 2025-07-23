using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Carnes.CarneModify
{
    /// <summary>
    /// Handler responsável por processar requisições de modificação de carnes (<see cref="CarneModifyCommand"/>).
    /// </summary>
    public class CarneModifyHandler : IRequestHandler<CarneModifyCommand, CarneResult>
    {
        private readonly ICarneRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CarneModifyHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de carnes.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public CarneModifyHandler(
            ICarneRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula a requisição de modificação de carne (<see cref="CarneModifyCommand"/>).
        /// </summary>
        /// <param name="command">Comando contendo os dados de modificação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da modificação, incluindo os dados atualizados.</returns>
        public async Task<CarneResult> Handle(CarneModifyCommand command, CancellationToken cancellationToken)
        {
            var carneToUpdate = await ValidateAsync(command, cancellationToken);

            carneToUpdate!.Update(
                command.Ean,
                command.Nome,
                command.TipoCarne,
                command.UnidadeMedida
            );

           
            var entity = await _repository.UpdateAsync(carneToUpdate, cancellationToken);

            return _mapper.Map<CarneResult>(entity);
        }

        /// <summary>
        /// Valida o comando <see cref="CarneModifyCommand"/> quanto às regras de negócio e integridade dos dados.
        /// </summary>
        /// <param name="command">Comando a ser validado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>A entidade Carne a ser modificada, se válida.</returns>
        /// <exception cref="ValidationException">Lançada quando as regras de validação falham.</exception>
        /// <exception cref="InvalidOperationException">Lançada quando o item não é encontrado ou existe conflito com outro EAN.</exception>
        private async Task<Carne> ValidateAsync(CarneModifyCommand command, CancellationToken cancellationToken)
        {
            var validator = new CarneModifyValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var carneToUpdate = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (carneToUpdate == null)
                throw new KeyNotFoundException($"Carne com ID {command.Id} não foi encontrado.");

            var sameEan = await _repository.FindAsync(p => p.Ean == command.Ean && p.Id != command.Id, cancellationToken);
            if (sameEan.Any())
                throw new InvalidOperationException($"Já existe outra carne com o EAN {command.Ean}.");

            return carneToUpdate;
        }
    }
}

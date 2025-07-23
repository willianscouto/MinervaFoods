using AutoMapper;
using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Estados.CarneGetAll
{
    /// <summary>
    /// Handler para o comando <see cref="CarneGetAllCommand"/>.
    /// Responsável por recuperar todas as carnes cadastradas.
    /// </summary>
    public class CarneGetAllHandler : IRequestHandler<CarneGetAllCommand, IEnumerable<CarneResult>>
    {
        private readonly ICarneRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler de recuperação de todas as carnes.
        /// </summary>
        /// <param name="repository">Repositório para acesso aos dados de carnes.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public CarneGetAllHandler(
            ICarneRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa a lógica para recuperar todas as carnes.
        /// </summary>
        /// <param name="command">Comando para recuperar todas as carnes (sem parâmetros).</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna uma lista de <see cref="CarneResult"/> contendo todas as carnes.</returns>
        public async Task<IEnumerable<CarneResult>> Handle(CarneGetAllCommand command, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CarneResult>>(entities);
        }
    }
}

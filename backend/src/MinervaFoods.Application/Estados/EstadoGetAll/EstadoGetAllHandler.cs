using AutoMapper;
using MediatR;
using MinervaFoods.Application.Estados.Common;
using MinervaFoods.Application.Estados.EstadoGetAll;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Estados.EstadoGetAll
{
    /// <summary>
    /// Handler para o comando <see cref="EstadoGetAllCommand"/>.
    /// Responsável por recuperar todos os estados cadastrados.
    /// </summary>
    public class EstadoGetAllHandler : IRequestHandler<EstadoGetAllCommand, IEnumerable<EstadoResult>>
    {
        private readonly IEstadoRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler de recuperação de todos os estados.
        /// </summary>
        /// <param name="repository">Repositório para acesso aos dados de estados.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public EstadoGetAllHandler(
            IEstadoRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa a lógica para recuperar todos os estados.
        /// </summary>
        /// <param name="command">Comando para recuperar todos os estados (sem parâmetros).</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna uma lista de <see cref="EstadoResult"/> contendo todos os estados.</returns>
        public async Task<IEnumerable<EstadoResult>> Handle(EstadoGetAllCommand command, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EstadoResult>>(entities);
        }
    }
}

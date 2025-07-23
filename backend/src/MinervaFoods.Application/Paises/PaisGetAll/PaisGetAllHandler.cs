using AutoMapper;
using MediatR;
using MinervaFoods.Application.Paises.Common;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Paises.PaisGetAll
{
    /// <summary>
    /// Handler para o comando <see cref="PaisGetAllCommand"/>.
    /// Responsável por recuperar todos os países cadastrados.
    /// </summary>
    public class PaisGetAllHandler : IRequestHandler<PaisGetAllCommand, IEnumerable<PaisResult>>
    {
        private readonly IPaisRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler de recuperação de todos os países.
        /// </summary>
        /// <param name="repository">Repositório para acesso aos dados de países.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public PaisGetAllHandler(
            IPaisRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa a lógica para recuperar todos os países.
        /// </summary>
        /// <param name="command">Comando para recuperar todos os países (sem parâmetros).</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna uma lista de <see cref="PaisResult"/> contendo todos os países.</returns>
        public async Task<IEnumerable<PaisResult>> Handle(PaisGetAllCommand command, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PaisResult>>(entities);
        }
    }
}

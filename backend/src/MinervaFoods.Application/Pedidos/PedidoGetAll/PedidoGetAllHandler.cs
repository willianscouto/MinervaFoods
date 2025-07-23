using AutoMapper;
using MediatR;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Application.Pedidos.Common;
using MinervaFoods.Application.Pedidos.PedidoGetAll;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Compradores.CompradorGetAll
{
    /// <summary>
    /// Handler para o comando <see cref="CompradorGetAllCommand"/>.
    /// Responsável por recuperar todos os compradores cadastrados.
    /// </summary>
    public class PedidoGetAllHandler : IRequestHandler<PedidoGetAllCommand, IEnumerable<PedidoResult>>
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do handler de recuperação de todos os compradores.
        /// </summary>
        /// <param name="repository">Repositório para acesso aos dados de compradores.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public PedidoGetAllHandler(
        IPedidoRepository repository,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executa a lógica para recuperar todos os compradores.
        /// </summary>
        /// <param name="command">Comando para recuperar todos os compradores (sem parâmetros).</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna uma lista de <see cref="CompradorResult"/> contendo todos os compradores.</returns>
        public async Task<IEnumerable<PedidoResult>> Handle(PedidoGetAllCommand command, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken,"Comprador", "PedidoItem", "PedidoItem.Carne");
            return _mapper.Map<IEnumerable<PedidoResult>>(entities);
        }
    }
}

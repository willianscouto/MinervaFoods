using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinervaFoods.Api.Common;
using MinervaFoods.Api.Features.Pedidos.Common;
using MinervaFoods.Api.Features.Pedidos.PedidoCreate;
using MinervaFoods.Api.Features.Pedidos.PedidoDelete;
using MinervaFoods.Api.Features.Pedidos.PedidoGet;
using MinervaFoods.Api.Features.Pedidos.PedidoModify;
using MinervaFoods.Application.Pedidos.PedidoCreate;
using MinervaFoods.Application.Pedidos.PedidoDelete;
using MinervaFoods.Application.Pedidos.PedidoGet;
using MinervaFoods.Application.Pedidos.PedidoGetAll;
using MinervaFoods.Application.Pedidos.PedidoModify;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Pedidos
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas aos Pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PedidosController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo Pedido.
        /// </summary>
        /// <param name="request">Dados do Pedido a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes do Pedido criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<PedidoResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async System.Threading.Tasks.Task<IActionResult> CreatePedido([FromBody] PedidoCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new PedidoCreateRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnCreatedAsync(async () =>
            {
                var command = _mapper.Map<PedidoCreateCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<PedidoResponse>(result);
            }, "Pedido criado com sucesso");
        }

        /// <summary>
        /// Modifica um Pedido existente.
        /// </summary>
        /// <param name="request">Dados do Pedido a ser modificado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes do Pedido modificado.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<PedidoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async System.Threading.Tasks.Task<IActionResult> ModifyPedido([FromBody] PedidoModifyRequest request, CancellationToken cancellationToken)
        {
            var validator = new PedidoModifyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<PedidoModifyCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<PedidoResponse>(result);
            }, "Pedido modificado com sucesso");
        }

        /// <summary>
        /// Remove um Pedido com base no seu ID.
        /// </summary>
        /// <param name="id">ID do Pedido a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Confirmação da remoção.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> DeletePedido([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new PedidoDeleteRequest { Id = id };
            var validator = new PedidoDeleteRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnVoidAsync(async () =>
            {
                var command = _mapper.Map<PedidoDeleteCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);
            }, "Pedido removido com sucesso");
        }

        /// <summary>
        /// Retorna todos os Pedidos cadastrados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de Pedidos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<PedidoResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await ReturnAsync(async () =>
            {
                var command = new PedidoGetAllCommand();
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<IEnumerable<PedidoResponse>>(result);
            }, "Pedidos recuperados com sucesso");
        }

        /// <summary>
        /// Retorna um Pedido com base no seu ID.
        /// </summary>
        /// <param name="id">ID do Pedido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes do Pedido encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<PedidoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new PedidoGetRequest { Id = id };
            var validator = new PedidoGetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<PedidoGetCommand>(request.Id);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<PedidoResponse>(result);
            }, "Pedido recuperado com sucesso");
        }
    }
}

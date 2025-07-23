using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinervaFoods.Api.Common;
using MinervaFoods.Api.Features.Compradores.Common;
using MinervaFoods.Api.Features.Compradores.CompradorCreate;
using MinervaFoods.Api.Features.Compradores.CompradorDelete;
using MinervaFoods.Api.Features.Compradores.CompradorGet;
using MinervaFoods.Api.Features.Compradores.CompradorModify;
using MinervaFoods.Application.Compradores.CompradorCreate;
using MinervaFoods.Application.Compradores.CompradorDelete;
using MinervaFoods.Application.Compradores.CompradorGet;
using MinervaFoods.Application.Compradores.CompradorGetAll;
using MinervaFoods.Application.Compradores.CompradorModify;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Compradores
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas aos Compradores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CompradoresController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CompradoresController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo Comprador.
        /// </summary>
        /// <param name="request">Dados do comprador a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dados do comprador criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CompradorResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async System.Threading.Tasks.Task<IActionResult> CreateComprador([FromBody] CompradorCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new CompradorCreateRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnCreatedAsync(async () =>
            {
                var command = _mapper.Map<CompradorCreateCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<CompradorResponse>(result);
            }, "Comprador criado com sucesso");
        }

        /// <summary>
        /// Atualiza os dados de um Comprador existente.
        /// </summary>
        /// <param name="request">Dados do comprador a ser atualizado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dados atualizados do comprador.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<CompradorResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async System.Threading.Tasks.Task<IActionResult> ModifyComprador([FromBody] CompradorModifyRequest request, CancellationToken cancellationToken)
        {
            var validator = new CompradorModifyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<CompradorModifyCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<CompradorResponse>(result);
            }, "Comprador atualizado com sucesso");
        }

        /// <summary>
        /// Remove um Comprador com base no seu ID.
        /// </summary>
        /// <param name="id">ID do comprador a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Confirmação de remoção do comprador.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> DeleteComprador([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new CompradorDeleteRequest { Id = id };
            var validator = new CompradorDeleteRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnVoidAsync(async () =>
            {
                var command = _mapper.Map<CompradorDeleteCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);
            }, "Comprador removido com sucesso");
        }

        /// <summary>
        /// Retorna todos os Compradores cadastrados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de compradores.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<CompradorResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await ReturnAsync(async () =>
            {
                var command = new CompradorGetAllCommand();
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<IEnumerable<CompradorResponse>>(result);
            }, "Compradores recuperados com sucesso");
        }

        /// <summary>
        /// Retorna os dados de um Comprador com base no seu ID.
        /// </summary>
        /// <param name="id">ID do comprador.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dados do comprador, se encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CompradorResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new CompradorGetRequest { Id = id };
            var validator = new CompradorGetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<CompradorGetCommand>(request.Id);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<CompradorResponse>(result);
            }, "Comprador recuperado com sucesso");
        }
    }
}

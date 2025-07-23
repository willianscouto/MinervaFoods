using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinervaFoods.Api.Common;
using MinervaFoods.Api.Features.Carnes.CarneCreate;
using MinervaFoods.Api.Features.Carnes.CarneDelete;
using MinervaFoods.Api.Features.Carnes.CarneGet;
using MinervaFoods.Api.Features.Carnes.CarneModify;
using MinervaFoods.Api.Features.Carnes.Common;
using MinervaFoods.Application.Carnes.CarneCreate;
using MinervaFoods.Application.Carnes.CarneDelete;
using MinervaFoods.Application.Carnes.CarneGet;
using MinervaFoods.Application.Carnes.CarneGetAll;
using MinervaFoods.Application.Carnes.CarneModify;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carnes
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas às carnes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CarnesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarnesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria uma nova carne.
        /// </summary>
        /// <param name="request">Dados da carne a ser criada.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes da carne criada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CarneResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async System.Threading.Tasks.Task<IActionResult> CreateCarne([FromBody] CarneCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new CarneCreateRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnCreatedAsync(async () =>
            {
                var command = _mapper.Map<CarneCreateCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<CarneResponse>(result);
            }, "Carne criada com sucesso");
        }

        /// <summary>
        /// Altera uma carne existente.
        /// </summary>
        /// <param name="request">Dados da carne a ser modificada.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes da carne modificada.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<CarneResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async System.Threading.Tasks.Task<IActionResult> ModifyCarne([FromBody] CarneModifyRequest request, CancellationToken cancellationToken)
        {
            var validator = new CarneModifyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<CarneModifyCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<CarneResponse>(result);
            }, "Carne modificada com sucesso");
        }

        /// <summary>
        /// Remove uma carne com base no seu ID.
        /// </summary>
        /// <param name="id">ID da carne a ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Confirmação de remoção.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> DeleteCarne([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new CarneDeleteRequest { Id = id };
            var validator = new CarneDeleteRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnVoidAsync(async () =>
            {
                var command = _mapper.Map<CarneDeleteCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);
            }, "Carne removida com sucesso");
        }

        /// <summary>
        /// Retorna todas as carnes cadastradas.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de carnes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<CarneResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await ReturnAsync(async () =>
            {
                var command = new CarneGetAllCommand();
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<IEnumerable<CarneResponse>>(result);
            }, "Carnes recuperadas com sucesso");
        }

        /// <summary>
        /// Retorna uma carne com base no seu ID.
        /// </summary>
        /// <param name="id">ID da carne.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes da carne encontrada.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CarneResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new CarneGetRequest { Id = id };
            var validator = new CarneGetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<CarneGetCommand>(request.Id);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<CarneResponse>(result);
            }, "Carne recuperada com sucesso");
        }
    }
}

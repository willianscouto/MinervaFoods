using AutoMapper;
using MinervaFoods.Api.Features.Estados.EstadoGet;
using MinervaFoods.Api.Features.Estados.EstadoGetByPais;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinervaFoods.Api.Common;
using MinervaFoods.Api.Features.Estados.Common;
using MinervaFoods.Application.Estados.EstadoGet;
using MinervaFoods.Application.Estados.EstadoGetAll;
using MinervaFoods.Application.Estados.EstadoGetByPais;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Projects
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas aos estados.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EstadoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna os dados de um estado com base no seu ID.
        /// </summary>
        /// <param name="id">Identificador único do estado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes do estado, se encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<EstadoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new EstadoGetRequest { Id = id };
            var validator = new EstadoGetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<EstadoGetCommand>(request.Id);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<EstadoResponse>(result);
            }, "Estado recuperado com sucesso");
        }

        /// <summary>
        /// Retorna todos os estados cadastrados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de estados.</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<EstadoResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await ReturnAsync(async () =>
            {
                var command = new EstadoGetAllCommand();
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<IEnumerable<EstadoResponse>>(result);
            }, "Estados recuperados com sucesso");
        }

        /// <summary>
        /// Retorna todos os estados de um país específico.
        /// </summary>
        /// <param name="paisId">Identificador único do país.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de estados pertencentes ao país.</returns>
        [HttpGet("GetByPais/{paisId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<EstadoResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetByPais([FromRoute] Guid paisId, CancellationToken cancellationToken)
        {
            var request = new EstadoGetByPaisRequest { PaisId = paisId };
            var validator = new EstadoGetByPaisRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<EstadoGetByPaisCommand>(request.PaisId);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map <IEnumerable<EstadoResponse>>(result);
            }, "Estados por país recuperados com sucesso");
        }
    }
}

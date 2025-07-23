using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinervaFoods.Api.Common;
using MinervaFoods.Api.Features.Paises.Common;
using MinervaFoods.Api.Features.Paises.PaisGet;
using MinervaFoods.Application.Paises.PaisGet;
using MinervaFoods.Application.Paises.PaisGetAll;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Projects
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas aos países.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaisController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaisController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna os dados de um país com base no seu ID.
        /// </summary>
        /// <param name="id">Identificador único do país.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Detalhes do país, se encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<PaisResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new PaisGetRequest { Id = id };
            var validator = new PaisGetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return await ReturnAsync(async () =>
            {
                var command = _mapper.Map<PaisGetCommand>(request.Id);
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<PaisResponse>(result);
            }, "País recuperado com sucesso");
        }

        /// <summary>
        /// Retorna todos os países cadastrados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de países.</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<PaisResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async System.Threading.Tasks.Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await ReturnAsync(async () =>
            {
                var command = new PaisGetAllCommand();
                var result = await _mediator.Send(command, cancellationToken);
                return _mapper.Map<IEnumerable<PaisResponse>>(result);
            }, "Países recuperados com sucesso");
        }
    }
}

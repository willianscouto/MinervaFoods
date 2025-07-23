using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Compradores.CompradorCreate
{
    /// <summary>
    /// Handler responsável por processar os comandos de criação de comprador.
    /// </summary>
    public class CompradorCreateHandler : IRequestHandler<CompradorCreateCommand, CompradorCreateResult>
    {
        private readonly ICompradorRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CompradorCreateHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório utilizado para persistência.</param>
        /// <param name="mapper">Instância do AutoMapper para mapear objetos.</param>
        public CompradorCreateHandler(
            ICompradorRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula o comando <see cref="CompradorCreateCommand"/>.
        /// Valida o comando, cria o comprador e retorna o resultado.
        /// </summary>
        /// <param name="command">Comando com os dados do comprador.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da criação contendo os dados do comprador criado.</returns>
        public async System.Threading.Tasks.Task<CompradorCreateResult> Handle(CompradorCreateCommand command, CancellationToken cancellationToken)
        {
            await ValidateAsync(command, cancellationToken);

            var comprador = _mapper.Map<Comprador>(command); 
            comprador = await _repository.CreateAsync(comprador, cancellationToken);

            return _mapper.Map<CompradorCreateResult>(comprador);
        }

        /// <summary>
        /// Valida o comando <see cref="CompradorCreateCommand"/> conforme as regras de negócio.
        /// </summary>
        /// <param name="command">Comando a ser validado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        private async System.Threading.Tasks.Task ValidateAsync(CompradorCreateCommand command, CancellationToken cancellationToken)
        {
            var validator = new CompradorCreateValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingEAN = await _repository.FindAsync(c => c.Documento == command.Documento, cancellationToken);
            if (existingEAN.Any())
                throw new InvalidOperationException($"Produto com EAN {command.Documento} já existe.");
        }
    }
}

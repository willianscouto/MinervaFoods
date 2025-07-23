using AutoMapper;
using FluentValidation;
using MediatR;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories;

namespace MinervaFoods.Application.Compradores.CompradorModify
{
    /// <summary>
    /// Handler responsável por processar requisições de modificação de compradores (<see cref="CompradorModifyCommand"/>).
    /// </summary>
    public class CompradorModifyHandler : IRequestHandler<CompradorModifyCommand, CompradorResult>
    {
        private readonly ICompradorRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CompradorModifyHandler"/>.
        /// </summary>
        /// <param name="repository">Repositório de compradores.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public CompradorModifyHandler(
            ICompradorRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula a requisição de modificação de comprador (<see cref="CompradorModifyCommand"/>).
        /// </summary>
        /// <param name="command">Comando contendo os dados de modificação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da modificação, incluindo os dados atualizados.</returns>
        public async Task<CompradorResult> Handle(CompradorModifyCommand command, CancellationToken cancellationToken)
        {
            var compradorToUpdate = await ValidateAsync(command, cancellationToken);

            compradorToUpdate!.Update(
                command.Documento,
                command.Nome,
                command.Email,
                command.Telefone,
                command.Logradouro,
                command.Complemento,
                command.Bairro,
                command.Estado,
                command.Cep,
                command.Pais,
                command.DataNascimento
            );

            var entity = await _repository.UpdateAsync(compradorToUpdate, cancellationToken);

            return _mapper.Map<CompradorResult>(entity);
        }

        /// <summary>
        /// Valida o comando <see cref="CompradorModifyCommand"/> quanto às regras de negócio e integridade dos dados.
        /// </summary>
        /// <param name="command">Comando a ser validado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>A entidade <see cref="Comprador"/> a ser modificada, se válida.</returns>
        /// <exception cref="ValidationException">Lançada quando as regras de validação falham.</exception>
        /// <exception cref="InvalidOperationException">Lançada quando o item não é encontrado ou existe conflito com outro documento.</exception>
        private async Task<Comprador> ValidateAsync(CompradorModifyCommand command, CancellationToken cancellationToken)
        {
            var validator = new CompradorModifyValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var compradorToUpdate = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (compradorToUpdate == null)
                throw new KeyNotFoundException($"Comprador com ID {command.Id} não foi encontrado.");

            var sameDocumento = await _repository.FindAsync(p => p.Documento == command.Documento && p.Id != command.Id, cancellationToken);
            if (sameDocumento.Any())
                throw new InvalidOperationException($"Já existe outro comprador com o documento {command.Documento}.");

            return compradorToUpdate;
        }
    }
}

using FluentValidation;
using MinervaFoods.Application.PedidosItens.PedidoItemModify;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.Pedidos.PedidoModify
{
    /// <summary>
    /// Validador para o comando <see cref="PedidoModifyCommand"/>, responsável por validar a atualização dos dados de um pedido.
    /// </summary>
    /// <remarks>
    /// Regras de validação aplicadas:
    /// <list type="bullet">
    /// <item><description><strong>Id</strong>: obrigatório e deve ser um GUID válido (diferente de Guid.Empty).</description></item>
    /// <item><description><strong>CompradorId</strong>: obrigatório.</description></item>
    /// <item><description><strong>DataPedido</strong>: não pode ser uma data futura.</description></item>
    /// <item><description><strong>StatusPedido</strong>: não pode ser igual a "Unknown".</description></item>
    /// <item><description><strong>PedidoItem</strong>: cada item da lista será validado por <see cref="PedidoItemModifyValidator"/>.</description></item>
    /// </list>
    /// </remarks>
    public class PedidoModifyValidator : AbstractValidator<PedidoModifyCommand>
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="PedidoModifyValidator"/> e define as regras de validação.
        /// </summary>
        public PedidoModifyValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do pedido deve ser um GUID válido.");

            RuleFor(p => p.CompradorId)
                .NotEmpty()
                .WithMessage("O identificador do comprador é obrigatório.");

            RuleFor(p => p.DataPedido)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do pedido não pode ser no futuro.");

            RuleFor(p => p.StatusPedido)
                .NotEqual(PedidoEnum.Status.Unknown)
                .WithMessage("Status do pedido inválido.");

            RuleForEach(p => p.PedidoItem)
                .SetValidator(new PedidoItemModifyValidator());
        }
    }
}

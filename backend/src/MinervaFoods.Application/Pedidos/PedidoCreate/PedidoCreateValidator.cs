using FluentValidation;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.Pedidos.PedidoCreate
{
    /// <summary>
    /// Validador para o comando <see cref="PedidoCreateCommand"/> que define regras para criação de um pedido.
    /// </summary>
    /// <remarks>
    /// Regras de validação incluem:
    /// <list type="bullet">
    /// <item><description><strong>CompradorId</strong>: Obrigatório.</description></item>
    /// <item><description><strong>DataPedido</strong>: Não pode ser uma data futura.</description></item>
    /// <item><description><strong>StatusPedido</strong>: Não pode ser 'Unknown'.</description></item>
    /// <item><description><strong>PedidoItem</strong>: Cada item deve seguir as regras do <see cref="PedidoItemCreateValidator"/>.</description></item>
    /// </list>
    /// </remarks>
    public class PedidoCreateValidator : AbstractValidator<PedidoCreateCommand>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoCreateValidator"/>
        /// com regras de validação definidas para criação de pedidos.
        /// </summary>
        public PedidoCreateValidator()
        {
            RuleFor(p => p.CompradorId)
                .NotEmpty()
                .WithMessage("O identificador do comprador é obrigatório.");

            RuleFor(p => p.DataPedido)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do pedido não pode ser no futuro.");

           
            RuleForEach(p => p.PedidoItem)
                .SetValidator(new PedidoItemCreateValidator());
        }
    }
}

using FluentValidation;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.PedidosItens.PedidoItemCreate
{
    /// <summary>
    /// Validador para o comando <see cref="PedidoItemCreateCommand"/> que define regras para a criação de um item de pedido.
    /// </summary>
    /// <remarks>
    /// As regras de validação incluem:
    /// <list type="bullet">
    /// <item><description><strong>PedidoId</strong>: Obrigatório. Deve ser um identificador válido.</description></item>
    /// <item><description><strong>CarneId</strong>: Obrigatório. Deve ser um identificador válido.</description></item>
    /// <item><description><strong>Quantidade</strong>: Obrigatória. Deve ser maior que zero.</description></item>
    /// <item><description><strong>PrecoUnitario</strong>: Obrigatório. Deve ser maior que zero.</description></item>
    /// <item><description><strong>Cotacao</strong>: Obrigatória. Deve ser maior que zero.</description></item>
    /// <item><description><strong>Moeda</strong>: Obrigatória. Deve estar entre os valores definidos no enum <see cref="Moeda"/>.</description></item>
    /// </list>
    /// </remarks>
    public class PedidoItemCreateValidator : AbstractValidator<PedidoItemCreateItem>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoItemCreateValidator"/>
        /// com regras de validação para criação de item de pedido.
        /// </summary>
        public PedidoItemCreateValidator()
        {


            RuleFor(i => i.CarneId)
                .NotEmpty()
                .WithMessage("O identificador da carne é obrigatório.");

            RuleFor(i => i.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(i => i.PrecoUnitario)
                .GreaterThan(0)
                .WithMessage("O preço unitário deve ser maior que zero.");


            RuleFor(c => c.Moeda)
               .IsInEnum().WithMessage("Tipo de moeda inválido.")
               .NotEqual(PedidoItemEnum.Moeda.Unknown).WithMessage("Tipo de moeda deve ser especificado.");

        }
    }
}

using MinervaFoods.Domain.Common;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Domain.Entities
{
    /// <summary>
    /// Representa um item de um pedido, associando uma carne, quantidade, preço e moeda.
    /// </summary>
    public class PedidoItem : BaseEntity
    {
        /// <summary>
        /// Identificador do pedido ao qual o item pertence.
        /// </summary>
        public Guid PedidoId { get; set; }

        /// <summary>
        /// Identificador da carne associada ao item.
        /// </summary>
        public Guid CarneId { get; private set; }

        public Carne Carne { get; private set; } = null!;

        /// <summary>
        /// Quantidade da carne no pedido (em KG).
        /// </summary>
        public decimal Quantidade { get; private set; }

        /// <summary>
        /// Moeda utilizada para o preço do item.
        /// </summary>
        public PedidoItemEnum.Moeda Moeda { get; private set; }

        /// <summary>
        /// Preço unitário da carne na moeda especificada.
        /// </summary>
        public decimal PrecoUnitario { get; private set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Total { get; private set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Cotacao { get; private set; }

        /// <summary>
        /// Valor total do item Total X Cotacao na moeda do item.
        /// </summary>
        public decimal ValorTotalCotacao { get; private set; }

        /// <summary>
        /// Construtor protegido para EF Core.
        /// </summary>
        public PedidoItem() { }

        /// <summary>
        /// Inicializa um novo item de pedido.
        /// </summary>
        /// <param name="pedidoId">Id do pedido.</param>
        /// <param name="carneId">Id da carne.</param>
        /// <param name="quantidade">Quantidade em KG.</param>
        /// <param name="precoUnitario">Preço unitário na moeda informada.</param>
        /// <param name="moeda">Moeda do preço.</param>
        public PedidoItem(Guid pedidoId, Guid carneId, decimal quantidade, decimal precoUnitario, PedidoItemEnum.Moeda moeda, decimal cotacao)
        {
            PedidoId = pedidoId;
            CarneId = carneId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Moeda = moeda;
            Total = quantidade * precoUnitario;
            Cotacao = cotacao;
            ValorEmReal();
        }

        /// <summary>
        /// Atualiza a quantidade e o preço unitário do item, recalculando o total.
        /// </summary>
        /// <param name="quantidade">Nova quantidade.</param>
        /// <param name="precoUnitario">Novo preço unitário.</param>
        /// <param name="cotacao">Cotação.</param>
        public void Atualizar(decimal quantidade, decimal precoUnitario, decimal cotacao)
        {

            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Total = quantidade * precoUnitario;
            Cotacao = cotacao;
            ValorEmReal();
        }


        public void AtualizarValorCotacao(decimal cotacao)
        {
            Cotacao = cotacao;
            Total = Quantidade * PrecoUnitario;
            ValorEmReal();
        }




        /// <summary>
        /// Calcula o valor total deste item convertido para Real, usando a taxa de câmbio fornecida.
        /// </summary>
        /// <param name="cotacao">Taxa de câmbio para conversão para Real.</param>
        /// <returns>Valor total convertido para Real.</returns>
        public void ValorEmReal()
        {
            ValorTotalCotacao = Total * Cotacao;
            
        }
    }
}

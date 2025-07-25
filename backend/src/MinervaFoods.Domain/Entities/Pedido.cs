using MinervaFoods.Domain.Common;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Domain.Validators;
using MinervaFoods.Helpers;

namespace MinervaFoods.Domain.Entities
{
    /// <summary>
    /// Representa um pedido realizado por um comprador, contendo uma lista de itens (carnes) e suas informações.
    /// </summary>
    public class Pedido : BaseEntity
    {
        /// <summary>
        /// Identificador do comprador que realizou o pedido.
        /// </summary>
        public Guid CompradorId { get; private set; }

        /// <summary>
        /// Identificador do comprador que realizou o pedido.
        /// </summary>
        public Comprador Comprador { get; private set; } = null!;

        /// <summary>
        /// Identificador do do numero do pedido.
        /// </summary>
        public long NumeroPedido { get; private set; }

        /// <summary>
        /// Data em que o pedido foi realizado.
        /// </summary>
        public DateTime DataPedido { get; private set; }

        /// <summary>
        /// Status atual do pedido (Aberto, Finalizado, Cancelado).
        /// </summary>
        public PedidoEnum.Status StatusPedido { get; private set; }

        /// <summary>
        /// Coleção dos itens (carnes) que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItem> PedidoItem { get; private set; } = new List<PedidoItem>();

        /// <summary>
        /// Valor total do pedido calculado com base nos itens.
        /// </summary>
        public decimal ValorTotal { get; private set; }


        /// <summary>
        /// Valor total do pedido calculado com base nos itens.
        /// </summary>
        public string Observacao { get; private set; } = string.Empty;

        /// <summary>
        /// Construtor protegido para uso do Entity Framework Core.
        /// </summary>
        public Pedido() { }

        /// <summary>
        /// Inicializa um novo pedido com comprador e data do pedido.
        /// </summary>
        /// <param name="compradorId">Identificador do comprador.</param>
        /// <param name="numeroPedido">NumeroPedido do comprador.</param>
        /// <param name="dataPedido">Data do pedido.</param>
        public Pedido(Guid compradorId,long numeroPedido, DateTime dataPedido, string observacao)
        {
            CompradorId = compradorId;
            DataPedido = dataPedido;
            StatusPedido = PedidoEnum.Status.Aberto;
            Observacao = observacao;
        }

        /// <summary>
        /// Inicializa um novo pedido com comprador e data do pedido.
        /// </summary>
        /// <param name="compradorId">Identificador do comprador.</param>
        /// <param name="numeroPedido">NumeroPedido do comprador.</param>
        /// <param name="dataPedido">Data do pedido.</param>
        public void Atualizar( string observacao, PedidoEnum.Status status)
        {
        
            StatusPedido = status;
            Observacao = observacao;
        }

        /// <summary>
        /// Adiciona um numero de pedido.
        /// </summary>
        /// <param name="numeroPedido">Item a ser adicionado.</param>
        public void AdicionarNumeroPedido(long numeroPedido)
        {
            NumeroPedido = numeroPedido;
        }

        /// <summary>
        /// Adiciona um item à coleção de itens do pedido e recalcula o valor total.
        /// </summary>
        /// <param name="item">Item a ser adicionado.</param>
        public void AdicionarItem(PedidoItem item)
        {
            PedidoItem.Add(item);
            RecalcularValorTotal();
        }

        /// <summary>
        /// Adiciona um item à coleção de itens do pedido e recalcula o valor total.
        /// </summary>
        /// <param name="item">Item a ser adicionado.</param>
        public void AdicionarItem(ICollection<PedidoItem> itens)
        {
            foreach (var item in itens)
              PedidoItem.Add(item);
            RecalcularValorTotal();
        }


        /// <summary>
        /// Remove um item da coleção de itens do pedido e recalcula o valor total.
        /// </summary>
        /// <param name="item">Item a ser removido.</param>
        public void RemoverItem(PedidoItem item)
        {
            PedidoItem.Remove(item);
            RecalcularValorTotal();
        }

        /// <summary>
        /// Remove um item da coleção de itens do pedido e recalcula o valor total.
        /// </summary>
        /// <param name="item">Item a ser removido.</param>
        public void ClearItens()
        {
            PedidoItem.Clear();
        }


        /// <summary>
        /// Recalcula o valor total do pedido somando os valores de todos os itens.
        /// </summary>
        private void RecalcularValorTotal()
        {
            ValorTotal = 0;
            foreach (var item in PedidoItem)
            {
                ValorTotal += item.ValorTotalCotacao;
            }
        }

        /// <summary>
        /// Marca o pedido como finalizado.
        /// </summary>
        public void Finalizar()
        {
            StatusPedido = PedidoEnum.Status.Finalizado;
        }

        /// <summary>
        /// Cancela o pedido.
        /// </summary>
        public void Cancelar()
        {
            StatusPedido = PedidoEnum.Status.Cancelado;
        }

        /// <summary>
        /// Valida os dados da pedido usando regras de negócio.
        /// </summary>
        /// <returns>
        /// Um <see cref="ValidationResultDetail"/> com o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validar()
        {
            var validator = new PedidoValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }

    }
}

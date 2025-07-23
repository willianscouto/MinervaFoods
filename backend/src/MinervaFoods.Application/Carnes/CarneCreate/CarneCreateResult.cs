namespace MinervaFoods.Application.Carnes.CarneCreate
{
    /// <summary>
    /// Representa a resposta retornada após a criação bem-sucedida de um novo comentário.
    /// </summary>
    /// <remarks>
    /// Essa resposta contém o identificador único do comentário recém-criado,
    /// que pode ser utilizado em operações futuras ou como referência.
    /// </remarks>
    public class CarneCreateResult
    {
        /// <summary>
        /// Obtém ou define o identificador único do comentário criado.
        /// </summary>
        /// <value>Um GUID que identifica exclusivamente o comentário criado no sistema.</value>
        public Guid Id { get; set; }
    }
}

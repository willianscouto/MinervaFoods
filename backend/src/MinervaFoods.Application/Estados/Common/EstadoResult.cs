﻿namespace MinervaFoods.Application.Estados.Common
{
    /// <summary>
    /// Representa o resultado da operação com a entidade Estado.
    /// </summary>
    public class EstadoResult
    {
        public Guid Id { get; set; }
        public Guid PaisId { get; set; }
        public string Sigla { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}

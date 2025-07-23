using MinervaFoods.Domain.Common;

namespace MinervaFoods.Domain.Entities
{
    public class Estado : BaseEntity
    {
        public Guid PaisId { get; private set; }
        public string Sigla { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;
        
        public Estado() { }

        public Estado(Guid paisId,string sigla, string nome)
        {
            PaisId = paisId;  
            Sigla = sigla;
            Nome = nome;
        }

        public void Atualizar(Guid paisId, string sigla, string nome)
        {
            PaisId = paisId;
            Sigla = sigla;
            Nome = nome;
        }
    }
}

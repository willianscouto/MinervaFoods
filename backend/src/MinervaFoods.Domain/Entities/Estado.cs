using MinervaFoods.Domain.Common;

namespace MinervaFoods.Domain.Entities
{
    public class Estado : BaseEntity
    {
        public Guid CodPais { get; private set; }
        public string Sigla { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;
        
        public Estado() { }

        public Estado(Guid codPais,string sigla, string nome)
        {
            CodPais = codPais;  
            Sigla = sigla;
            Nome = nome;
        }

        public void Atualizar(Guid codPais, string sigla, string nome)
        {
            CodPais = codPais;
            Sigla = sigla;
            Nome = nome;
        }
    }
}

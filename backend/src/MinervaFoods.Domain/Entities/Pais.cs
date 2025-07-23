using MinervaFoods.Domain.Common;

namespace MinervaFoods.Domain.Entities
{
    public class Pais : BaseEntity
    {
        public string Sigla { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;

        public Pais() { }

        public Pais(string sigla, string nome)
        {
            Sigla = sigla;
            Nome = nome;
        }

        public void Atualizar(string sigla, string nome)
        {
            Sigla = sigla;
            Nome = nome;
        }
    }
}

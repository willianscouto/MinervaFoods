using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinervaFoods.Data.Migrations
{
    /// <inheritdoc />
    public partial class FakeRegistros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userIdCreated = Guid.Parse("e4a5dbd9-b2e5-4a67-9ad4-8c9c84a57043");
            var now = DateTime.Now;

            // Inserir País: Brasil
            var brasilId = Guid.NewGuid();
            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "Id", "Nome", "Sigla", "Status", "IdUserCreated", "CreatedAt" },
                values: new object[] { brasilId, "Brasil", "BR", true, userIdCreated, now });

            // Inserir Estados do Brasil
            var estadosBrasil = new (string Nome, string Sigla)[]
            {
                ("Acre", "AC"), ("Alagoas", "AL"), ("Amapá", "AP"), ("Amazonas", "AM"), ("Bahia", "BA"),
                ("Ceará", "CE"), ("Distrito Federal", "DF"), ("Espírito Santo", "ES"), ("Goiás", "GO"),
                ("Maranhão", "MA"), ("Mato Grosso", "MT"), ("Mato Grosso do Sul", "MS"), ("Minas Gerais", "MG"),
                ("Pará", "PA"), ("Paraíba", "PB"), ("Paraná", "PR"), ("Pernambuco", "PE"), ("Piauí", "PI"),
                ("Rio de Janeiro", "RJ"), ("Rio Grande do Norte", "RN"), ("Rio Grande do Sul", "RS"),
                ("Rondônia", "RO"), ("Roraima", "RR"), ("Santa Catarina", "SC"), ("São Paulo", "SP"),
                ("Sergipe", "SE"), ("Tocantins", "TO")
            };

            foreach (var estado in estadosBrasil)
            {
                migrationBuilder.InsertData(
                    table: "Estados",
                    columns: new[] { "Id", "Nome", "Sigla", "PaisId", "Status", "IdUserCreated", "CreatedAt" },
                    values: new object[] { Guid.NewGuid(), estado.Nome, estado.Sigla, brasilId, true, userIdCreated, now });
            }

            // Inserir outros países
            var paises = new[]
            {
                (Id: Guid.NewGuid(), Nome: "Estados Unidos", Sigla: "US"),
                (Id: Guid.NewGuid(), Nome: "Canadá", Sigla: "CA"),
                (Id: Guid.NewGuid(), Nome: "Alemanha", Sigla: "DE"),
                (Id: Guid.NewGuid(), Nome: "Japão", Sigla: "JP"),
                (Id: Guid.NewGuid(), Nome: "Reino Unido", Sigla: "GB"),
                (Id: Guid.NewGuid(), Nome: "China", Sigla: "CN"),
                (Id: Guid.NewGuid(), Nome: "França", Sigla: "FR"),
                (Id: Guid.NewGuid(), Nome: "Austrália", Sigla: "AU")
            };

            foreach (var pais in paises)
            {
                migrationBuilder.InsertData(
                    table: "Paises",
                    columns: new[] { "Id", "Nome", "Sigla", "Status", "IdUserCreated", "CreatedAt" },
                    values: new object[] { pais.Id, pais.Nome, pais.Sigla, true, userIdCreated, now });
            }

            // Inserir estados principais desses países
            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Nome", "Sigla", "PaisId", "Status", "IdUserCreated", "CreatedAt" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Califórnia", "CA", paises[0].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Nova York", "NY", paises[0].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Texas", "TX", paises[0].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Ontário", "ON", paises[1].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Quebec", "QC", paises[1].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Baviera", "BY", paises[2].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Tóquio", "TK", paises[3].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Inglaterra", "ENG", paises[4].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Pequim", "BJ", paises[5].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Île-de-France", "IDF", paises[6].Id, true, userIdCreated, now },
                    { Guid.NewGuid(), "Nova Gales do Sul", "NSW", paises[7].Id, true, userIdCreated, now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Estados");
            migrationBuilder.Sql("DELETE FROM Paises");
        }
    }
}

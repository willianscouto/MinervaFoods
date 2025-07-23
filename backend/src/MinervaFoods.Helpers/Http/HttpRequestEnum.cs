using System.ComponentModel;

namespace MinervaFoods.Helpers.Http
{
    
    public static class HttpRequestEnum
    {
        public enum StatusCode
        {
            [Description("A solicitação foi bem-sucedida.")]
            OK = 200,

            [Description("A solicitação foi bem-sucedida e um novo recurso foi criado.")]
            Created = 201,

            [Description("A solicitação foi aceita para processamento, mas ainda não concluída.")]
            Accepted = 202,

            [Description("Nenhum conteúdo para retornar, mas a solicitação foi bem-sucedida.")]
            NoContent = 204,

            [Description("A solicitação foi malformada ou inválida.")]
            BadRequest = 400,

            [Description("A autenticação é necessária para obter a resposta solicitada.")]
            Unauthorized = 401,

            [Description("O cliente não tem permissão para acessar o recurso.")]
            Forbidden = 403,

            [Description("O recurso solicitado não foi encontrado.")]
            NotFound = 404,

            [Description("O método HTTP não é suportado para este recurso.")]
            MethodNotAllowed = 405,

            [Description("Erro interno no servidor.")]
            InternalServerError = 500,

            [Description("O servidor não suporta a funcionalidade necessária para atender à solicitação.")]
            NotImplemented = 501,

            [Description("O servidor está temporariamente indisponível, geralmente devido a manutenção.")]
            ServiceUnavailable = 503
        }
    }
}

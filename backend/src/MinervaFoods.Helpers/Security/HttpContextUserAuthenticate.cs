namespace MinervaFoods.Helpers.Security
{
    /// <summary>
    /// Default implementation of <see cref="IUserAuthenticate"/> using the HTTP context.
    /// Currently returns mock values for testing purposes.
    /// </summary>
    public class HttpContextUserAuthenticate : IUserAuthenticate
    {
        public Guid UserId => Guid.Parse("e4a5dbd9-b2e5-4a67-9ad4-8c9c84a57043");

        public string Username => "leonardo.teste@minervafoods.com.br";

        public string Role => "Adm";

        public bool IsInRole(string role) =>
            string.Equals(role, "Adm", StringComparison.OrdinalIgnoreCase);
    }
}

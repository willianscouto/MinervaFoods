namespace MinervaFoods.Helpers.Security
{
    /// <summary>
    /// Provides access to information about the currently authenticated user.
    /// </summary>
    public interface IUserAuthenticate
    {
        /// <summary>
        /// Gets the unique identifier of the currently authenticated user.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Gets the username or email of the currently authenticated user.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the role of the currently authenticated user.
        /// </summary>
        string Role { get; }

        /// <summary>
        /// Determines whether the current user is in the specified role.
        /// </summary>
        /// <param name="role">The role to check.</param>
        /// <returns>True if the user is in the given role; otherwise, false.</returns>
        bool IsInRole(string role);
    }
}

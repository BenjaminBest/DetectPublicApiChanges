namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface ISourceControlCredentials defines credentials to be used for source control authentification
    /// </summary>
    public interface ISourceControlCredentials
    {
        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        string User { get; }
    }
}
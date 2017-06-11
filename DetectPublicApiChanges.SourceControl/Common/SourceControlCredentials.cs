using DetectPublicApiChanges.SourceControl.Interfaces;

namespace DetectPublicApiChanges.SourceControl.Common
{
    /// <summary>
    /// SourceControlCredentials contains all information needed to login to a subversion server
    /// </summary>
    public class SourceControlCredentials : ISourceControlCredentials
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlCredentials" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The user.</param>
        public SourceControlCredentials(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}

using System;
using DetectPublicApiChanges.SourceControl.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.SourceControl.Tests.Common
{
    [TestClass]
    public class SourceControlConfigurationTests
    {
        [TestMethod]
        public void Parse_ShouldCreateGitConfiguration_WhenNoCredentialsUsed()
        {
            const string connnectionstring = "Git;Url;StartRev;EndRev";

            var expectedConfiguration =
                new SourceControlConfiguration("StartRev", "EndRev", "Url", SourceControlType.Git);

            var configuration = SourceControlConfiguration.Parse(connnectionstring);
            configuration.ShouldBeEquivalentTo(expectedConfiguration);
        }

        [TestMethod]
        public void Parse_ShouldCreateSvnConfiguration_WhenNoCredentialsUsed()
        {
            const string connnectionstring = "Svn;Url;StartRev;EndRev";

            var expectedConfiguration =
                new SourceControlConfiguration("StartRev", "EndRev", "Url", SourceControlType.Svn);

            var configuration = SourceControlConfiguration.Parse(connnectionstring);
            configuration.ShouldBeEquivalentTo(expectedConfiguration);
        }

        [TestMethod]
        public void Parse_ShouldCreateGitConfiguration_WhenCredentialsUsed()
        {
            const string connnectionstring = "Git;Url;StartRev;EndRev;User;Password";

            var expectedConfiguration =
                new SourceControlConfiguration("StartRev", "EndRev", "Url", SourceControlType.Git)
                {
                    Credentials = new SourceControlCredentials("User", "Password")
                };

            var configuration = SourceControlConfiguration.Parse(connnectionstring);
            configuration.ShouldBeEquivalentTo(expectedConfiguration);
        }

        [TestMethod]
        public void Parse_ShouldCreateSvnConfiguration_WhenCredentialsUsed()
        {
            const string connnectionstring = "Svn;Url;StartRev;EndRev;User;Password";

            var expectedConfiguration =
                new SourceControlConfiguration("StartRev", "EndRev", "Url", SourceControlType.Svn)
                {
                    Credentials = new SourceControlCredentials("User", "Password")
                };

            var configuration = SourceControlConfiguration.Parse(connnectionstring);
            configuration.ShouldBeEquivalentTo(expectedConfiguration);
        }

        [TestMethod]
        public void Parse_ShouldThrowArgumentException_WhenConnectionstringIsIvalid()
        {
            const string connnectionstring = "Svn;Url";


            Action act = () => SourceControlConfiguration.Parse(connnectionstring);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void Parse_ShouldThrowArgumentException_WhenConnectionstringIsEmpty()
        {
            var connnectionstring = string.Empty;


            Action act = () => SourceControlConfiguration.Parse(connnectionstring);
            act.ShouldThrow<ArgumentException>();
        }
    }
}

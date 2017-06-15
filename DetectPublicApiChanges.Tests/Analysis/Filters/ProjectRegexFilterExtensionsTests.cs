using System.Linq;
using DetectPublicApiChanges.Analysis.Filters;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.Filters
{
    [TestClass]
    public class ProjectRegexFilterExtensionsTests
    {
        private static Project GetProject(string name, AdhocWorkspace workspace)
        {
            var projectId = ProjectId.CreateNewId();
            var versionStamp = VersionStamp.Create();
            var projectInfo = ProjectInfo.Create(projectId, versionStamp, name, name, LanguageNames.CSharp);

            return workspace.AddProject(projectInfo);
        }

        private static AdhocWorkspace GetWorkspace()
        {
            var workspace = new AdhocWorkspace();

            GetProject("My.Namespace.Project", workspace);
            GetProject("My.Namespace.Project2", workspace);
            GetProject("My.Namespace.Project1.Tests", workspace);

            return workspace;
        }

        [TestMethod]
        public void Filter_ShouldReturnFilteredList_WhenNameMatchFilterIsUsed()
        {
            var projects = GetWorkspace().CurrentSolution.Projects;

            projects.Filter(@"\.Tests").Count().Should().Be(2);
        }
    }
}

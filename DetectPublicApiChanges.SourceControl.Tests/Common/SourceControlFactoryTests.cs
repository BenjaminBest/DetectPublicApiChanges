using System;
using System.Collections.Generic;
using DetectPublicApiChanges.SourceControl.Common;
using DetectPublicApiChanges.SourceControl.Interfaces;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DetectPublicApiChanges.SourceControl.Tests.Common
{
    [TestClass]
    public class SourceControlFactoryTests
    {
        private static ILog CreateLogMock()
        {
            var mock = new Mock<ILog>();

            return mock.Object;
        }

        private static ISourceControlClient CreateSvnMock()
        {
            var client = new Mock<ISourceControlClient>();
            client.Setup(item => item.Type).Returns(SourceControlType.Svn);

            return client.Object;
        }

        private static ISourceControlClient CreateGitMock()
        {
            var client = new Mock<ISourceControlClient>();
            client.Setup(item => item.Type).Returns(SourceControlType.Git);

            return client.Object;
        }

        private static IEnumerable<ISourceControlClient> CreateClientsMock()
        {
            return new List<ISourceControlClient> { CreateGitMock(), CreateSvnMock() };
        }

        [TestMethod]
        public void CreateClient_ShouldThrowArgumentNullException_WhenConfigurationIsNull()
        {
            var factory = new SourceControlFactory(CreateClientsMock(), CreateLogMock());

            Action act = () => factory.CreateClient(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateClient_ShouldThrowArgumentException_WhenTypeIsNotFound()
        {
            var factory = new SourceControlFactory(new[] { CreateGitMock() }, CreateLogMock());

            var config = new SourceControlConfiguration("StartRev", "EndRev", "Url", SourceControlType.Svn);

            Action act = () => factory.CreateClient(config);

            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void CreateClient_ShouldReturnGitClient_WhenGitConfigurationIsUsed()
        {
            var factory = new SourceControlFactory(CreateClientsMock(), CreateLogMock());

            var config = new SourceControlConfiguration("StartRev", "EndRev", "Url", SourceControlType.Git);

            factory.CreateClient(config).Type.Should().Be(SourceControlType.Git);
        }
    }
}
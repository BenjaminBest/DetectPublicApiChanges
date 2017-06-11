using System;
using DetectPublicApiChanges.SourceControl.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.SourceControl.Tests.Common
{
    [TestClass]
    public class SourceControlChangeLogTests
    {
        [TestMethod]
        public void SourceControlChangeLog_ShouldHaveAnEmptyListOfItem_WhenInitiallyRequested()
        {
            var changelog = new SourceControlChangeLog(string.Empty, string.Empty);
            changelog.Items.Count.Should().Be(0);
        }

        [TestMethod]
        public void AddItem_ShouldAddItemToTheList_WhenNotNull()
        {
            var changelog = new SourceControlChangeLog(string.Empty, string.Empty);

            var item = new SourceControlChangeLogItem("Author", "Message", DateTime.Now);

            changelog.AddItem(item);

            changelog.Items.Should().Contain(item);
            changelog.Items.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddItem_ShouldNotAddItemToTheList_WhenNull()
        {
            var changelog = new SourceControlChangeLog(string.Empty, string.Empty);
            changelog.AddItem(null);

            changelog.Items.Count.Should().Be(0);
        }
    }
}

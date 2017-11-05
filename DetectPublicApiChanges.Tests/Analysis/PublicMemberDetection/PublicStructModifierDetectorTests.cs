using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.PublicMemberDetection
{
    [TestClass]
    public class PublicStructModifierDetectorTests
    {
        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenStructIsPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<StructDeclarationSyntax>(TestCase.PublicStruct, "TestStruct");

            new PublicStructModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenPublicStructIsInsideInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<StructDeclarationSyntax>(TestCase.InternalClass, "TestStruct");

            new PublicStructModifierDetector().IsPublic(node).Should().BeFalse();
        }
    }
}
using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.PublicMemberDetection
{
    [TestClass]
    public class PublicInterfaceModifierDetectorTests
    {
        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenInterfaceIsPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicInterface, "ITestInterface");

            new PublicInterfaceModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenInterfaceIsPublicButInsideInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicInterfaceInsideInternalClass, "ITestInterface");

            new PublicInterfaceModifierDetector().IsPublic(node).Should().BeFalse();
        }
    }
}
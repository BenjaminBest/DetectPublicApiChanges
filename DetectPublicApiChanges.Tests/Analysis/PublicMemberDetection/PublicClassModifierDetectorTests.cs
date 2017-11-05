using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.PublicMemberDetection
{
    [TestClass]
    public class PublicClassModifierDetectorTests
    {
        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenClassIsPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            new PublicClassModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenClassIsNotPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.InternalClass, "TestClass");

            new PublicClassModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenClassIsInternalAndInsideNotPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.InternalClassInsidePublicClass, "InternalTestClass");

            new PublicClassModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenClassIsNestedAndPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherPublicTestClass");

            new PublicClassModifierDetector().IsPublic(node).Should().BeTrue();
        }
    }
}

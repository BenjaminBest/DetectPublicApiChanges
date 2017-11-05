using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.PublicMemberDetection
{
    [TestClass]
    public class PublicMethodModifierDetectorTests
    {
        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenMethodIsPublicInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethod");

            new PublicMethodModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenMethodIsPublicInNestedPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "InnerTestMethod");

            new PublicMethodModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenMethodIsExplicitlyPrivate()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "ExplicitePrivateTestMethod");

            new PublicMethodModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenMethodIsImplicitlyPrivate()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "ImplicitePrivateTestMethod");

            new PublicMethodModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenMethodIsInPublicInterface()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicInterface, "TestMethod");

            new PublicMethodModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenMethodIsInInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.InternalClass, "TestMethod");

            new PublicMethodModifierDetector().IsPublic(node).Should().BeFalse();
        }
    }
}
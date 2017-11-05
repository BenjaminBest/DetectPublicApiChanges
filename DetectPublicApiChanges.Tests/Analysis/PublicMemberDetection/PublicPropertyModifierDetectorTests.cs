using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.PublicMemberDetection
{
    [TestClass]
    public class PublicPropertyModifierDetectorTests
    {
        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenPropertyIsInPublicInterface()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicInterface, "TestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenPropertyIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicClass, "TestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenStaticPropertyIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.NestedPublicClasses, "TestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenStaticPropertyIsInPublicNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherTestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenPropertyIsInPrivateClassInsidePublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PrivateClassInsidePublicClass, "PrivateTestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenPropertyIsInInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.InternalClass, "TestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenPropertyIsInInternalClassInsidePublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.InternalClass, "InternalTestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenGenericPropertyIsInPublicGenericInterface()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicGenericInterface, "TestProperty");

            new PublicPropertyModifierDetector().IsPublic(node).Should().BeTrue();
        }
    }
}
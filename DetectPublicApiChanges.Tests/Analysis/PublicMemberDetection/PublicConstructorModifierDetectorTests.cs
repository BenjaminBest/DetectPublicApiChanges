using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.PublicMemberDetection
{
    [TestClass]
    public class PublicConstructorModifierDetectorTests
    {
        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WheCtorIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            new PublicConstructorModifierDetector().IsPublic(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenCtorIsNotInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.InternalClass, "TestClass");

            new PublicConstructorModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnFalse_WhenCtorIsInClassWhichIsInternalAndInsideNotPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.InternalClassInsidePublicClass, "InternalTestClass");

            new PublicConstructorModifierDetector().IsPublic(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsPublic_ShouldReturnTrue_WhenCtorIsInClassWhichIsNestedAndPublic()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherPublicTestClass");

            new PublicConstructorModifierDetector().IsPublic(node).Should().BeTrue();
        }
    }
}

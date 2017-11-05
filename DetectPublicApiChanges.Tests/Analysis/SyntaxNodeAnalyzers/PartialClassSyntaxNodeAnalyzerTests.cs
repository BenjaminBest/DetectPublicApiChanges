using DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.SyntaxNodeAnalyzers
{
    [TestClass]
    public class PartialClassSyntaxNodeAnalyzerTests
    {
        private static readonly IIndexItemFactory IndexItemFactory = MockCreator.CreateIndexItemFactoryMock();

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenPartialClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicPartialClass, "TestClass");

            new PartialClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            new PartialClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenGenericClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicGenericClass, "TestClass");

            new PartialClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }
    }
}

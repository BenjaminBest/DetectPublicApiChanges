using System;
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
    public class InterfaceSyntaxNodeAnalyzerTests
    {
        private static readonly IIndexItemFactory IndexItemFactory = MockCreator.CreateIndexItemFactoryMock();

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenInterfaceIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicInterface, "ITestInterface");

            new InterfaceSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenGenericInterfaceIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicGenericClass, "ITestInterface");

            new InterfaceSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            new InterfaceSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void CreateItem_ShouldThrowException_WhenNodeIsNull()
        {
            Action act = () => new InterfaceSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(null);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsInterface()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicInterface, "ITestInterface");

            var item = new InterfaceSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.ITestInterface");
        }
    }
}
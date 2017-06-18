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
    public class ClassSyntaxNodeAnalyzerTests
    {
        private static readonly IIndexItemFactory IndexItemFactory = MockCreator.CreateIndexItemFactoryMock();

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            new ClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenPartialClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicPartialClass, "TestClass");

            new ClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenGenericClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicGenericClass, "TestClass");

            new ClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenInterfaceIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicInterface, "ITestInterface");

            new ClassSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void CreateItem_ShouldThrowException_WhenNodeIsNull()
        {
            Action act = () => new ClassSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(null);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            var item = new ClassSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.TestClass");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherPublicTestClass");

            var item = new ClassSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.TestClass.OtherPublicTestClass");
        }
    }
}
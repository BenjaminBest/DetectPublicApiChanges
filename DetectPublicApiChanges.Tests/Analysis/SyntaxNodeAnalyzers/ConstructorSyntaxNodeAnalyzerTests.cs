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
    public class ConstructorSyntaxNodeAnalyzerTests
    {
        private static readonly IIndexItemFactory IndexItemFactory = MockCreator.CreateIndexItemFactoryMock();

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenCtorIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenCtorInPartialClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicPartialClass, "TestClass");

            new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenCtorInGenericClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicGenericClass, "TestClass");

            new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenStaticCtorIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicStaticClass, "TestClass");

            new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void CreateItem_ShouldThrowException_WhenNodeIsNull()
        {
            Action act = () => new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(null);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsCtorInClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            var item = new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.TestClass.TestClass");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsCtorInNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherPublicTestClass");

            var item = new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.TestClass.OtherPublicTestClass.OtherPublicTestClass");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsCtorWith1Argument()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.CtorWith1ArgumentInPublicClass, "TestClass");

            var item = new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.TestClass.TestClassstringargument");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsCtorWith2Arguments()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.CtorWith2ArgumentsInPublicClass, "TestClass");

            var item = new ConstructorSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("Test.TestCases.TestClass.TestClassstringargument1intargument2");
        }
    }
}

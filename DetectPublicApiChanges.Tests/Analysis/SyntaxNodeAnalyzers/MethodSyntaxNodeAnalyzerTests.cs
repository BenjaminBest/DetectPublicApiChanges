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
    public class MethodSyntaxNodeAnalyzerTests
    {
        private static readonly IIndexItemFactory IndexItemFactory = MockCreator.CreateIndexItemFactoryMock();

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenMethodIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicClass, "TestMethod");

            new MethodSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenMethodInPartialClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicPartialClass, "TestMethod");

            new MethodSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenMethodInGenericClassIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicGenericClass, "TestMethodGeneric");

            new MethodSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenPropertyIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicGenericClass, "TestProperty");

            new MethodSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnFalse_WhenStaticMethodIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "StaticTestMethodWithArgument");

            new MethodSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeFalse();
        }

        [TestMethod]
        public void IsDeclarationSyntaxTypeSupported_ShouldReturnTrue_WhenMethodWithGenericReturnTypeIsTested()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethodWithGenericReturnType");

            new MethodSyntaxNodeAnalyzer(IndexItemFactory).IsDeclarationSyntaxTypeSupported(node).Should().BeTrue();
        }

        [TestMethod]
        public void CreateItem_ShouldThrowException_WhenNodeIsNull()
        {
            Action act = () => new MethodSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(null);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsMethodInClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethod");

            var item = new MethodSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("boolTest.TestCases.TestClass.TestMethod");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsMethodWith1Argument()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethodWith1Argument");

            var item = new MethodSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("boolTest.TestCases.TestClass.TestMethodWith1Argumentstringargument");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsMethodWith2Arguments()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethodWith2Arguments");

            var item = new MethodSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("boolTest.TestCases.TestClass.TestMethodWith2Argumentsstringargument1intargument2");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsMethodWithRefArgument()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethodWithRefArgument");

            var item = new MethodSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("boolTest.TestCases.TestClass.TestMethodWithRefArgumentrefstringargument");
        }

        [TestMethod]
        public void CreateItem_ShouldReturnItemWithValidKey_WhenNodeIsMethodWithOutArgument()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.Methods, "TestMethodWithOutArgument");

            var item = new MethodSyntaxNodeAnalyzer(IndexItemFactory).CreateItem(node);
            item.Key.Should().Be("boolTest.TestCases.TestClass.TestMethodWithOutArgumentoutstringargument");
        }

        //TODO:Nested
    }
}
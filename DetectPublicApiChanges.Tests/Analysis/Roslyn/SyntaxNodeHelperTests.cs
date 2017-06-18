using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DetectPublicApiChanges.Tests.Analysis.Roslyn
{
    [TestClass]
    public class SyntaxNodeHelperTests
    {
        [TestMethod]
        public void TryGetParentSyntax_ShouldReturnNamespaceDefinitionForCtor_WhenCtorIsUsed()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            NamespaceDeclarationSyntax namespaceDeclarationSyntax;
            SyntaxNodeHelper.TryGetParentSyntax(node, out namespaceDeclarationSyntax);

            namespaceDeclarationSyntax.Name.ToString().Should().Be("Test.TestCases");
        }

        [TestMethod]
        public void TryGetParentSyntax_ShouldReturnNamespaceDefinitionForClass_WhenClassIsUsed()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            NamespaceDeclarationSyntax namespaceDeclarationSyntax;
            SyntaxNodeHelper.TryGetParentSyntax(node, out namespaceDeclarationSyntax);

            namespaceDeclarationSyntax.Name.ToString().Should().Be("Test.TestCases");
        }

        [TestMethod]
        public void TryGetParentSyntax_ShouldReturnNamespaceDefinitionForProperty_WhenPropertyIsUsed()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicClass, "TestProperty");

            NamespaceDeclarationSyntax namespaceDeclarationSyntax;
            SyntaxNodeHelper.TryGetParentSyntax(node, out namespaceDeclarationSyntax);

            namespaceDeclarationSyntax.Name.ToString().Should().Be("Test.TestCases");
        }

        [TestMethod]
        public void TryGetParentSyntax_ShouldReturnNamespaceDefinitionForMethod_WhenMethodIsUsed()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicClass, "TestMethod");

            NamespaceDeclarationSyntax namespaceDeclarationSyntax;
            SyntaxNodeHelper.TryGetParentSyntax(node, out namespaceDeclarationSyntax);

            namespaceDeclarationSyntax.Name.ToString().Should().Be("Test.TestCases");
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenCtorIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenClassIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenPropertyIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicClass, "TestProperty");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenMethodIsInPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicClass, "TestMethod");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenCtorIsInInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.InternalClass, "TestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenClassIsInInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.InternalClass, "TestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenPropertyIsInInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.InternalClass, "TestProperty");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenMethodIsInInternalClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.InternalClass, "TestMethod");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenCtorIsInInternalSealedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.InternalSealedClass, "TestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenClassIsInInternalSealedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.InternalSealedClass, "TestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenPropertyIsInInternalSealedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.InternalSealedClass, "TestProperty");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenMethodIsInInternalSealedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.InternalSealedClass, "TestMethod");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenCtorIsPrivateClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PrivateClassInsidePublicClass, "PrivateTestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenClassIsPrivateClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PrivateClassInsidePublicClass, "PrivateTestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenPropertyIsPrivateClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PrivateClassInsidePublicClass, "PrivateTestProperty");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenMethodIsPrivateClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PrivateClassInsidePublicClass, "PrivateTestMethod");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenCtorIsInNestedPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherPublicTestClass");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenClassIsInNestedPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherPublicTestClass");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenPropertyIsInNestedPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherTestProperty");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenMethodIsInNestedPublicClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.NestedPublicClasses, "OtherTestMethod");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenCtorIsInPublicStruct()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.PublicStruct, "TestStruct");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenClassIsInPublicStruct()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<StructDeclarationSyntax>(TestCase.PublicStruct, "TestStruct");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenPropertyIsInPublicStruct()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicStruct, "TestProperty");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnTrue_WhenMethodIsInPublicStruct()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicStruct, "TestMethod");

            node.IsHierarchyPublic().Should().BeTrue();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenCtorIsInternalNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ConstructorDeclarationSyntax>(TestCase.InternalClassInsidePublicClass, "InternalTestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenClassIsInternalNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.InternalClassInsidePublicClass, "InternalTestClass");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenPropertyIsInternalNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.InternalClassInsidePublicClass, "InternalTestProperty");

            node.IsHierarchyPublic().Should().BeFalse();
        }

        [TestMethod]
        public void IsHierarchyPublic_ShouldReturnFalse_WhenMethodIsInternalNestedClass()
        {
            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.InternalClassInsidePublicClass, "InternalTestMethod");

            node.IsHierarchyPublic().Should().BeFalse();
        }
    }
}
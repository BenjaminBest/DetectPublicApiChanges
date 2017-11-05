using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Analysis.StructureIndex;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Tests.TestCases;
using DetectPublicApiChanges.Tests.TestCases.Helpers;
using FluentAssertions;
using log4net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DetectPublicApiChanges.Tests.Analysis.StructureIndex
{
    [TestClass]
    public class StructureIndexComparatorTest
    {
        private readonly List<IPublicModifierDetector> _modifierDetectors = new List<IPublicModifierDetector>();
        private readonly List<IStructureIndexSourceItemComparator> _sourceComparators = new List<IStructureIndexSourceItemComparator>();
        private readonly List<IStructureIndexTargetItemComparator> _targetComparators = new List<IStructureIndexTargetItemComparator>();
        private readonly ILog _log = new Mock<ILog>().Object;
        private readonly IDiagnosticAnalyzerDescriptor _descriptor = new Mock<IDiagnosticAnalyzerDescriptor>().Object;
        private StructureIndexComparator _comparator;
        private DetectPublicApiChanges.Analysis.StructureIndex.StructureIndex _sourceIndex;
        private DetectPublicApiChanges.Analysis.StructureIndex.StructureIndex _targetIndex;

        [TestInitialize]
        public void Initialize()
        {
            _modifierDetectors.Add(new PublicClassModifierDetector());
            _modifierDetectors.Add(new PublicInterfaceModifierDetector());
            _modifierDetectors.Add(new PublicConstructorModifierDetector());
            _modifierDetectors.Add(new PublicMethodModifierDetector());
            _modifierDetectors.Add(new PublicPropertyModifierDetector());
            _modifierDetectors.Add(new PublicStructModifierDetector());
            _sourceComparators.Add(new ItemSourceKeyComparator());
            _targetComparators.Add(new ItemTargetInterfacePropertyComparator());
            _targetComparators.Add(new ItemTargetInterfaceMethodComparator());
        }

        public void ClassInitialize()
        {
            _comparator = new StructureIndexComparator(_log, _modifierDetectors, _sourceComparators, _targetComparators);
            _sourceIndex = new DetectPublicApiChanges.Analysis.StructureIndex.StructureIndex(_log);
            _targetIndex = new DetectPublicApiChanges.Analysis.StructureIndex.StructureIndex(_log);

            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");
            _sourceIndex.AddOrUpdateItem(new IndexItem("KeyExists", node, _descriptor));
            _targetIndex.AddOrUpdateItem(new IndexItem("KeyExists", node, _descriptor));
        }

        [TestMethod]
        public void Compare_ShouldHaveDifference_WhenClassIsNotInTheTargetIndex()
        {
            ClassInitialize();

            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");
            _sourceIndex.AddOrUpdateItem(new IndexItem("Key", node, _descriptor));

            _comparator.Compare(_sourceIndex, _targetIndex).HasDifferences.Should().BeTrue();
        }

        [TestMethod]
        public void Compare_ShouldReturnOneDifference_WhenClassIsNotInTheTargetIndex()
        {
            ClassInitialize();

            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<ClassDeclarationSyntax>(TestCase.PublicClass, "TestClass");
            _sourceIndex.AddOrUpdateItem(new IndexItem("Key", node, _descriptor));

            _comparator.Compare(_sourceIndex, _targetIndex).HasDifferences.Should().BeTrue();
            _comparator.Compare(_sourceIndex, _targetIndex).Differences.FirstOrDefault().Key.Should().Be("Key");
        }

        [TestMethod]
        public void Compare_ShouldHaveNoDifferences_WhenClassIsInBothIndexes()
        {
            ClassInitialize();

            _comparator.Compare(_sourceIndex, _targetIndex).HasDifferences.Should().BeFalse();
        }

        [TestMethod]
        public void Compare_ShouldReturnOneDifference_WhenInterfaceIsNotInTheTargetIndex()
        {
            ClassInitialize();

            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<InterfaceDeclarationSyntax>(TestCase.PublicInterface, "ITestInterface");
            _sourceIndex.AddOrUpdateItem(new IndexItem("Key", node, _descriptor));

            _comparator.Compare(_sourceIndex, _targetIndex).HasDifferences.Should().BeTrue();
            _comparator.Compare(_sourceIndex, _targetIndex).Differences.FirstOrDefault().Key.Should().Be("Key");
        }

        [TestMethod]
        public void Compare_ShouldReturnOneDifference_WhenInterfacePropertyWasAdded()
        {
            ClassInitialize();

            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<PropertyDeclarationSyntax>(TestCase.PublicInterface, "TestProperty");
            _targetIndex.AddOrUpdateItem(new IndexItem("Key", node, _descriptor));

            _comparator.Compare(_sourceIndex, _targetIndex).HasDifferences.Should().BeTrue();
            _comparator.Compare(_sourceIndex, _targetIndex).Differences.FirstOrDefault().Key.Should().Be("Key");
        }

        [TestMethod]
        public void Compare_ShouldReturnOneDifference_WhenInterfaceMethodWasAdded()
        {
            ClassInitialize();

            var node = SyntaxNodeTestHelper.GetSyntaxNodeByName<MethodDeclarationSyntax>(TestCase.PublicInterface, "TestMethod");
            _targetIndex.AddOrUpdateItem(new IndexItem("Key", node, _descriptor));

            _comparator.Compare(_sourceIndex, _targetIndex).HasDifferences.Should().BeTrue();
            _comparator.Compare(_sourceIndex, _targetIndex).Differences.FirstOrDefault().Key.Should().Be("Key");
        }
    }
}

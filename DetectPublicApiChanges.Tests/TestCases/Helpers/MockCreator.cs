using DetectPublicApiChanges.Analysis.StructureIndex;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Moq;

namespace DetectPublicApiChanges.Tests.TestCases.Helpers
{
    /// <summary>
    /// The class MockCreator creates mocks for unit testing
    /// </summary>
    public class MockCreator
    {
        public static IIndexItemFactory CreateIndexItemFactoryMock()
        {
            var mock = new Mock<IIndexItemFactory>();
            mock.Setup(m => m.CreateItem(It.IsAny<string>(), It.IsAny<SyntaxNode>(),
                    It.IsAny<IDiagnosticAnalyzerDescriptor>()))
                .Returns((string key, SyntaxNode syntaxNode, IDiagnosticAnalyzerDescriptor descriptor) => new IndexItem(
                    key,
                    syntaxNode, descriptor));

            return mock.Object;
        }
    }
}

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The  interface IRootTypeStructureBuilder defines methods to convert or build a syntaxtree into the internal object structure
    /// </summary>
    /// <typeparam name="TSyntaxTree">The type of the syntax tree.</typeparam>
    /// <typeparam name="TStructure">The type of the structure.</typeparam>
    public interface IRootTypeStructureBuilder<in TSyntaxTree, out TStructure>
        where TSyntaxTree : SyntaxTree
        where TStructure : class
    {
        IEnumerable<TStructure> Build(TSyntaxTree tree);
    }
}
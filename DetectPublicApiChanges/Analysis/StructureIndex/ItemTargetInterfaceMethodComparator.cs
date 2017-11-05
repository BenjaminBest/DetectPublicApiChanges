using DetectPublicApiChanges.Extensions;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The class ItemTargetInterfaceMethodComparator check if the target is a method of an interface and is new
    /// </summary>
    /// <seealso cref="IStructureIndexTargetItemComparator" />
    public class ItemTargetInterfaceMethodComparator : IStructureIndexTargetItemComparator
    {
        /// <summary>
        /// Determines whether there are breaking changes between the <paramref name="source"/> and the <paramref name="target"/>
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if the target has breaking changes, otherwise <c>false</c>.
        /// </returns>
        public bool HasBreakingChanges(IIndexItem target, IIndexItem source)
        {
            if (source != null)
                return false;

            var item =
                (target.SyntaxNode as MethodDeclarationSyntax).IsNotNull(t => t.Parent as InterfaceDeclarationSyntax);

            return item != null;
        }
    }
}
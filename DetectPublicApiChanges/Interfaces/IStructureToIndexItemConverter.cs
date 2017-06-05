using System.Collections.Generic;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureToIndexItemConverter defines a converter for structure to index items
    /// </summary>
    public interface IStructureToIndexItemConverter<in TStructure>
        where TStructure : IStructure
    {
        /// <summary>
        /// Converts the specified structure graph to a list of structure index items, so the graph will be flattened
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        IEnumerable<IStructureIndexItem> Convert(TStructure structure, IStructure parent);
    }
}
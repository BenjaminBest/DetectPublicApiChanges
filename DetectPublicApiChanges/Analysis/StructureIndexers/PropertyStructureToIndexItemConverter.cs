using System.Collections.Generic;
using System.Text;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndexers
{
    /// <summary>
    /// The PropertyStructureToIndexItemConverter converts a structure to an index item
    /// </summary>
    public class PropertyStructureToIndexItemConverter : IStructureToIndexItemConverter<PropertyStructure>
    {
        /// <summary>
        /// Converts the specified structure to an index item.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public IEnumerable<IStructureIndexItem> Convert(PropertyStructure structure, IStructure parent)
        {
            return new[] { new StructureIndexItem(CreateIndexKey(structure, parent), structure, parent) };
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private static string CreateIndexKey(PropertyStructure structure, IStructure parent)
        {
            var parentNameSpace = string.Empty;
            var classStructure = parent as ClassStructure;
            if (classStructure != null)
                parentNameSpace = classStructure.FullName;
            else if (parent is InterfaceStructure)
                parentNameSpace = ((InterfaceStructure)parent).FullName;

            var key = new StringBuilder(parentNameSpace);

            key.Append(structure.Type);
            key.Append(structure.Name);

            return key.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Text;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndexers
{
    /// <summary>
    /// The ConstructorStructureToIndexItemConverter converts a structure to an index item
    /// </summary>
    public class ConstructorStructureToIndexItemConverter : IStructureToIndexItemConverter<ConstructorStructure>
    {
        /// <summary>
        /// Converts the specified structure to an index item.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public IEnumerable<IStructureIndexItem> Convert(ConstructorStructure structure, IStructure parent)
        {
            return new[] { new StructureIndexItem(CreateIndexKey(structure, parent), structure, parent) };
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private static string CreateIndexKey(ConstructorStructure structure, IStructure parent)
        {
            var parentNameSpace = string.Empty;
            var classStructure = parent as ClassStructure;
            if (classStructure != null)
                parentNameSpace = classStructure.FullName;
            else if (parent is InterfaceStructure)
                parentNameSpace = ((InterfaceStructure)parent).FullName;

            var key = new StringBuilder(parentNameSpace);

            foreach (var param in structure.Parameters)
            {
                key.Append(param.Name);
                key.Append(param.Type);
            }

            return key.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Text;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndexers
{
    /// <summary>
    /// The MethodStructureToIndexItemConverter converts a structure to an index item
    /// </summary>
    public class MethodStructureToIndexItemConverter : IStructureToIndexItemConverter<MethodStructure>
    {
        /// <summary>
        /// Converts the specified structure to an index item.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public IEnumerable<IStructureIndexItem> Convert(MethodStructure structure, IStructure parent)
        {
            return new[] { new StructureIndexItem(CreateIndexKey(structure, parent), structure, parent) };
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private static string CreateIndexKey(MethodStructure structure, IStructure parent)
        {
            var parentNameSpace = string.Empty;
            var classStructure = parent as ClassStructure;
            if (classStructure != null)
                parentNameSpace = classStructure.FullName;
            else if (parent is InterfaceStructure)
                parentNameSpace = ((InterfaceStructure)parent).FullName;

            var key = new StringBuilder(parentNameSpace);

            key.Append(structure.ReturnType);
            key.Append(structure.Name);

            foreach (var param in structure.Parameters)
            {
                key.Append(param.Name);
                key.Append(param.Type);
            }

            return key.ToString();
        }
    }
}

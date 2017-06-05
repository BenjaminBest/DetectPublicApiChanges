using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndexers
{
    /// <summary>
    /// The InterfaceStructureToIndexItemConverter converts a structure to an index item
    /// </summary>
    public class InterfaceStructureToIndexItemConverter : IStructureToIndexItemConverter<InterfaceStructure>
    {
        /// <summary>
        /// The method converter
        /// </summary>
        private readonly IStructureToIndexItemConverter<MethodStructure> _methodConverter;

        /// <summary>
        /// The property converter
        /// </summary>
        private readonly IStructureToIndexItemConverter<PropertyStructure> _propertyConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassStructureToIndexItemConverter" /> class.
        /// </summary>
        /// <param name="methodConverter">The method converter.</param>
        /// <param name="propertyConverter">The property converter.</param>
        public InterfaceStructureToIndexItemConverter(
            IStructureToIndexItemConverter<MethodStructure> methodConverter,
            IStructureToIndexItemConverter<PropertyStructure> propertyConverter)
        {
            _methodConverter = methodConverter;
            _propertyConverter = propertyConverter;
        }


        /// <summary>
        /// Converts the specified structure to an index item.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Structure has not the correct type for this converter</exception>
        public IEnumerable<IStructureIndexItem> Convert(InterfaceStructure structure, IStructure parent)
        {
            if (structure == null)
                throw new ArgumentException("Structure has not the correct type for this converter");

            var results = new List<IStructureIndexItem>
            {
                new StructureIndexItem(CreateIndexKey(structure), structure,parent)
            };

            results.AddRange(structure.Methods.SelectMany(s => _methodConverter.Convert(s, structure)));
            results.AddRange(structure.Properties.SelectMany(s => _propertyConverter.Convert(s, structure)));

            return results;
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <returns></returns>
        private static string CreateIndexKey(InterfaceStructure structure)
        {
            var key = new StringBuilder();
            key.Append(structure.FullName);

            return key.ToString();
        }
    }
}

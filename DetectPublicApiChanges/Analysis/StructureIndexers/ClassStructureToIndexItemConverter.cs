using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndexers
{
    /// <summary>
    /// The ClassStructureToIndexItemConverter converts a structure to an index item
    /// </summary>
    public class ClassStructureToIndexItemConverter : IStructureToIndexItemConverter<ClassStructure>
    {
        /// <summary>
        /// The constructor converter
        /// </summary>
        private readonly IStructureToIndexItemConverter<ConstructorStructure> _constructorConverter;

        /// <summary>
        /// The method converter
        /// </summary>
        private readonly IStructureToIndexItemConverter<MethodStructure> _methodConverter;

        /// <summary>
        /// The property converter
        /// </summary>
        private readonly IStructureToIndexItemConverter<PropertyStructure> _propertyConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassStructureToIndexItemConverter"/> class.
        /// </summary>
        /// <param name="constructorConverter">The constructor converter.</param>
        /// <param name="methodConverter">The method converter.</param>
        /// <param name="propertyConverter">The property converter.</param>
        public ClassStructureToIndexItemConverter(
            IStructureToIndexItemConverter<ConstructorStructure> constructorConverter,
            IStructureToIndexItemConverter<MethodStructure> methodConverter,
            IStructureToIndexItemConverter<PropertyStructure> propertyConverter)
        {
            _constructorConverter = constructorConverter;
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
        public IEnumerable<IStructureIndexItem> Convert(ClassStructure structure, IStructure parent)
        {
            if (structure == null)
                throw new ArgumentException("Structure has not the correct type for this converter");

            var results = new List<IStructureIndexItem>
            {
                new StructureIndexItem(CreateIndexKey(structure), structure, parent)
            };

            results.AddRange(structure.Constructors.SelectMany(s => _constructorConverter.Convert(s, structure)));
            results.AddRange(structure.Methods.SelectMany(s => _methodConverter.Convert(s, structure)));
            results.AddRange(structure.Properties.SelectMany(s => _propertyConverter.Convert(s, structure)));

            return results;
        }


        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <returns></returns>
        private static string CreateIndexKey(ClassStructure structure)
        {
            var key = new StringBuilder();
            key.Append(structure.FullName);

            return key.ToString();
        }
    }
}

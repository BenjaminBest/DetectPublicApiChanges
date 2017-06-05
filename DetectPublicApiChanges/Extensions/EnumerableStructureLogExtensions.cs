using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using log4net;

namespace DetectPublicApiChanges.Extensions
{
    /// <summary>
    /// The class EnumerableStructureLogExtensions contains extensions to log root structure elements and their decendents
    /// </summary>
    public static class EnumerableStructureLogExtensions
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public static ILog Logger
        {
            get;
            set;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<ClassStructure> Log(this IEnumerable<ClassStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Info($"\t\tAdd class '{item.Name}'");
                item.Modifiers.Log();
                item.Constructors.Log();
                item.Methods.Log();
                item.Properties.Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<InterfaceStructure> Log(this IEnumerable<InterfaceStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Info($"\t\tAdd interface '{item.Name}'");
                item.Modifiers.Log();
                item.Methods.Log();
                item.Properties.Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<ConstructorStructure> Log(this IEnumerable<ConstructorStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\tAdd ctor with {item.Parameters.Count()} params and {item.Modifiers.Count()} modifiers");
                item.Parameters.Log();
                item.Modifiers.Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<MethodStructure> Log(this IEnumerable<MethodStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\tAdd method '{item.Name}' of type {item.ReturnType} with {item.Parameters.Count()} params and {item.Modifiers.Count()} modifiers");
                item.Parameters.Log();
                item.Modifiers.Log();
            }

            return items;
        }


        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<PropertyStructure> Log(this IEnumerable<PropertyStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\tAdd property '{item.Name}' of type {item.Type} with {item.Modifiers.Count()} modifiers");
                item.Modifiers.Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<ParameterStructure> Log(this IEnumerable<ParameterStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\t\tAdd param '{item.Name}' of type {item.Type}");
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<ModifierStructure> Log(this IEnumerable<ModifierStructure> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\t\tAdd modifier '{item.Name}'");
            }

            return items;
        }

        /// <summary>
        /// Logs the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static SolutionStructure Log(this SolutionStructure item)
        {
            Logger.Info($"Add solution '{item.Name}'");

            return item;
        }

        /// <summary>
        /// Logs the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static ProjectStructure Log(this ProjectStructure item)
        {
            Logger.Info($"\tAdd project '{item.Name}'");

            return item;
        }
    }
}
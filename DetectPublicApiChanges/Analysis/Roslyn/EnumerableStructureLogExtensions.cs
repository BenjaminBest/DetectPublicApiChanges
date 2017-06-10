using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
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
        public static IEnumerable<ClassDeclarationSyntax> Log(this IEnumerable<ClassDeclarationSyntax> items)
        {
            foreach (var item in items)
            {
                Logger.Info($"\t\tAdd class '{item.GetName()}'");
                item.Modifiers.Log();
                item.GetConstructors().Log();
                item.GetMethods().Log();
                item.GetProperties().Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<InterfaceDeclarationSyntax> Log(this IEnumerable<InterfaceDeclarationSyntax> items)
        {
            foreach (var item in items)
            {
                Logger.Info($"\t\tAdd interface '{item.GetName()}'");
                item.Modifiers.Log();
                item.GetMethods().Log();
                item.GetProperties().Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<ConstructorDeclarationSyntax> Log(this IEnumerable<ConstructorDeclarationSyntax> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\tAdd ctor with {item.GetParameters().Count()} params and {item.Modifiers.Count()} modifiers");
                item.GetParameters().Log();
                item.Modifiers.Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<MethodDeclarationSyntax> Log(this IEnumerable<MethodDeclarationSyntax> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\tAdd method '{item.Identifier.Text}' of type {item.ReturnType} with {item.GetParameters().Count()} params and {item.Modifiers.Count} modifiers");
                item.GetParameters().Log();
                item.Modifiers.Log();
            }

            return items;
        }


        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<PropertyDeclarationSyntax> Log(this IEnumerable<PropertyDeclarationSyntax> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\tAdd property '{item.Identifier.Text}' of type {item.Type} with {item.Modifiers.Count()} modifiers");
                item.Modifiers.Log();
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<ParameterSyntax> Log(this IEnumerable<ParameterSyntax> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\t\tAdd param '{item.Identifier.Text}' of type {item.Type}");
            }

            return items;
        }

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<SyntaxToken> Log(this IEnumerable<SyntaxToken> items)
        {
            foreach (var item in items)
            {
                Logger.Debug($"\t\t\t\tAdd modifier '{item.ValueText}'");
            }

            return items;
        }

        /// <summary>
        /// Logs the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Solution Log(this Solution item)
        {
            Logger.Info($"Add solution '{item.Id}'");

            return item;
        }

        /// <summary>
        /// Logs the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Project Log(this Project item)
        {
            Logger.Info($"\tAdd project '{item.Name}'");

            return item;
        }
    }
}
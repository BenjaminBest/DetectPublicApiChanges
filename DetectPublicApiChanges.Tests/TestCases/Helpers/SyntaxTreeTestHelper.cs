using System;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DetectPublicApiChanges.Tests.TestCases.Helpers
{
    /// <summary>
    /// The class SyntaxTreeTestHelper contains helpers for unit tests
    /// </summary>
    public class SyntaxTreeTestHelper
    {
        /// <summary>
        /// Gets the syntax tree.
        /// </summary>
        /// <param name="testcase">The testcase.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static SyntaxTree GetSyntaxTree(string testcase)
        {
            var content = ReadFile(testcase);

            if (string.IsNullOrEmpty(content))
                throw new ArgumentException($"The testcase '{testcase}' seems not to be existent");

            var tree = CSharpSyntaxTree.ParseText(content);

            return tree;
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="filename">The filename without txt.</param>
        /// <returns></returns>
        private static string ReadFile(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(filename))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
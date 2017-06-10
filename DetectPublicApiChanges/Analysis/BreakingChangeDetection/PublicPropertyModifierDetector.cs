using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.BreakingChangeDetection
{
    /// <summary>
    /// The PublicPropertyModifierDetector detects if the structure is of type <see cref="PropertyStructure"/> and is public
    /// </summary>
    public class PublicPropertyModifierDetector : IPublicModifierDetector
    {
        /// <summary>
        /// Determines whether the specified structure is public.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <returns>
        ///   <c>true</c> if the specified structure is public; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPublic(IStructure structure)
        {
            var item = structure as PropertyStructure;

            return item != null && item.Modifiers.Any(m => m.Name.ToLower().Equals("public") || m.Name.ToLower().Equals("protected"));
        }
    }
}

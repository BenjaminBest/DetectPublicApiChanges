namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IPublicModifierDetector detects if the structure is public
    /// </summary>
    public interface IPublicModifierDetector
    {
        /// <summary>
        /// Determines whether the specified structure is public.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <returns>
        ///   <c>true</c> if the specified structure is public; otherwise, <c>false</c>.
        /// </returns>
        bool IsPublic(IStructure structure);
    }
}
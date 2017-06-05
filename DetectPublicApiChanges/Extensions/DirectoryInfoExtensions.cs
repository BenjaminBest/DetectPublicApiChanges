using System.IO;

namespace DetectPublicApiChanges.Extensions
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Ensures the given directly exists
        /// </summary>
        /// <param name="directory"></param>
        public static void EnsureExits(this DirectoryInfo directory)
        {
            if(!directory.Exists)
            {
                directory.Create();
            }          
        }
    }
}

using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// Handles reading/writing and querying the file system
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Writes the text to the specified file
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="path">The path of the file to write to</param>
        /// <param name="append">If true, write the text to the end of the file, otherwise overrides any existing file</param>
        /// <returns></returns>
        Task WriteAllTextToFileAsync(string text, string path, bool append = false);

        /// <summary>
        /// Normalizing a path based on the current operating system
        /// For Windows, replace forward slash with backslash. For Mac, replace backslash with forward slash.
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns></returns>
        string NormalizePath(string path);

        /// <summary>
        /// Resolves any relative elements of the path to absolute
        /// </summary>
        /// <param name="path">The path to resolve</param>
        /// <returns></returns>
        string ResolvePath(string path);
    }
}

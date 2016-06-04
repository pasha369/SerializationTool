using SerializationClient.Models;

namespace SerializationClient.Core.FIleWriter
{
    /// <summary>
    /// Represents file writer interface.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Create folder with internal folders and files.
        /// </summary>
        /// <param name="folder">Folder model.</param>
        /// <param name="outFolderPath">Output folder path.</param>
        void WriteFolderModel(FolderModel folder, string outFolderPath);

        /// <summary>
        /// Create file.
        /// </summary>
        /// <param name="filePath">Output file path.</param>
        /// <param name="file">File model.</param>
        /// <returns></returns>
        string WriteFileModel(string filePath, FileModel file);
    }
}

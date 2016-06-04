using System.IO;
using SerializationClient.Models;

namespace SerializationClient.Core.SerializeClients
{
    /// <summary>
    /// Represents serialize client interface.
    /// </summary>
    public interface ISerializeClient
    {
        /// <summary>
        /// Serialize file.
        /// </summary>
        /// <param name="file">File info.</param>
        /// <param name="serializedFilePath">Path to serialized file.</param>
        void SerializeFile(FileInfo file, string serializedFilePath);

        /// <summary>
        /// Deserialize file.
        /// </summary>
        /// <param name="serializedFilePath">Path to serialized file.</param>
        FileModel DeserializeFile(string serializedFilePath);

        /// <summary>
        /// Serialize folder.
        /// </summary>
        /// <param name="folderPath">Path to folder</param>
        /// <param name="outFile">Path where save serialized file.</param>
        void SerializeFolder(string folderPath, string outFile);

        /// <summary>
        /// Deserialize folder with subfolder and files.
        /// </summary>
        /// <param name="serializedFilePath">Path to serialized file.</param>
        FolderModel DeserializeFolder(string serializedFilePath);
    }
}

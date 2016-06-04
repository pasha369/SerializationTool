using System.IO;
using SerializationClient.Core.FIleWriter;
using SerializationClient.Core.SerializeClients;
using SerializationClient.Models;

namespace SerializationClient
{
    /// <summary>
    /// Represents serialize client wrapper.
    /// </summary>
    public class SerializeClientWrapper
    {
        /// <summary>
        /// Gets or sets serialize client.
        /// </summary>
        public ISerializeClient SerializeClient { get; set; }

        /// <summary>
        /// Gets or sets file writer.
        /// </summary>
        public IFileWriter FileWriter { get; set; }

        /// <summary>
        /// Gets or sets SerializedFilePath.
        /// </summary>
        public string SerializedFilePath { get; set; }

        /// <summary>
        /// Serialize file.
        /// </summary>
        /// <param name="file">File info.</param>
        public void SerializeFile(FileInfo file)
        {
            SerializeClient.SerializeFile(file, SerializedFilePath);
        }

        /// <summary>
        /// Deserialize file.
        /// </summary>
        /// <param name="filePath">Output file path.</param>
        /// <returns>File fullname.</returns>
        public string DeserializeFile(string filePath)
        {
            var file = SerializeClient.DeserializeFile(SerializedFilePath);
            var fullName = FileWriter.WriteFileModel(filePath, file);

            return fullName;
        }

        /// <summary>
        /// Serialize folder.
        /// </summary>
        /// <param name="folderPath"></param>
        public void SerializeFolder(string folderPath)
        {
            SerializeClient.SerializeFolder(folderPath, SerializedFilePath);
        }

        /// <summary>
        /// Deserialize folder.
        /// </summary>
        /// <param name="folderOutPath">Folder output path.</param>
        public void DeserializeFolder(string folderOutPath)
        {
            var folder = SerializeClient.DeserializeFolder(SerializedFilePath);
            FileWriter.WriteFolderModel(folder, folderOutPath);
        }

        /// <summary>
        /// Deserialize folder.
        /// </summary>
        /// <param name="serializedFilePath">Folder output path.</param>
        public FolderModel DeserializeFolderModel(string serializedFilePath)
        {
            return SerializeClient.DeserializeFolder(serializedFilePath);
        }

        public void SaveFolder(FolderModel folder, string outputPath)
        {
            FileWriter.WriteFolderModel(folder, outputPath);
        }
    }
}

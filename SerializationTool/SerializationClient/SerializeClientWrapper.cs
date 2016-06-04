using System.IO;
using System.Threading.Tasks;
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
        /// Initialize SerializeClientWrapper instance.
        /// </summary>
        /// <param name="serializeClient">Serialize client.</param>
        /// <param name="fileWriter">File writer.</param>
        public SerializeClientWrapper(ISerializeClient serializeClient, IFileWriter fileWriter)
        {
            FileWriter = fileWriter;
            SerializeClient = serializeClient;
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
        /// Serialize folder asynchronously.
        /// </summary>
        /// <param name="folderPath">Serialize folder path.</param>
        public async Task SerializeFolderAsync(string folderPath)
        {
            SerializeClient.SerializeFolder(folderPath, SerializedFilePath);
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
        /// <param name="serializedFilePath">Serialized file path.</param>
        public FolderModel DeserializeFolderModel(string serializedFilePath)
        {
            return SerializeClient.DeserializeFolder(serializedFilePath);
        }

        /// <summary>
        /// Save folder.
        /// </summary>
        /// <param name="folder">Folder model</param>
        /// <param name="outputPath">Output path.</param>
        public void SaveFolder(FolderModel folder, string outputPath)
        {
            FileWriter.WriteFolderModel(folder, outputPath);
        }
    }
}

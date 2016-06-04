using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SerializationClient.Extension;
using SerializationClient.Models;

namespace SerializationClient.Core.SerializeClients
{
    /// <summary>
    /// Represent binary serialize client.
    /// </summary>
    public class BinarySerializeClient : ISerializeClient
    {
        protected BinaryFormatter BinaryFormatter { get; set; }

        /// <summary>
        /// Initialize BinarySerializeClient instance.
        /// </summary>
        public BinarySerializeClient()
        {
            this.BinaryFormatter = new BinaryFormatter();
        }

        public void SerializeFile(FileInfo file, string serializedFilePath)
        {
            var fileModel = new FileModel();
            fileModel.Name = file.Name;
            fileModel.DataBytes = File.ReadAllBytes(file.FullName);
            SerializeObject(serializedFilePath, fileModel);
        }

        public FileModel DeserializeFile(string serializedFilePath)
        {
            FileModel fileModel;
            using (Stream inStream = new FileStream(serializedFilePath,
                                                    FileMode.Open,
                                                    FileAccess.Read,
                                                    FileShare.Read))
            {
                var t = BinaryFormatter.Deserialize(inStream);
                inStream.Position = 0;
                fileModel = BinaryFormatter.Deserialize(inStream) as FileModel;
            }
            return fileModel;
        }

        public void SerializeFolder(string folderPath, string outFile)
        {
            var dir = new DirectoryInfo(folderPath);
            var folderModel = dir.ConvertToFolderModel();

            SerializeObject(outFile, folderModel);
        }

        public FolderModel DeserializeFolder(string serializedFilePath)
        {
            FolderModel folder;
            using (Stream inStream = new FileStream(serializedFilePath,
                                                    FileMode.Open,
                                                    FileAccess.Read,
                                                    FileShare.Read))
            {
                folder = BinaryFormatter.Deserialize(inStream) as FolderModel;
                
            }
            return folder;
        }

        private void SerializeObject<T>(string outFile, T target)
        {
            using (Stream outStream = new FileStream(outFile,
                                                     FileMode.OpenOrCreate,
                                                     FileAccess.Write,
                                                     FileShare.None))
            {
                BinaryFormatter.Serialize(outStream, target);
            }
        }
    }
}
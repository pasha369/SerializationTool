using System.IO;
using SerializationClient.Models;

namespace SerializationClient.Extension
{
    /// <summary>
    /// Represents file extensions.
    /// </summary>
    public static class FileExt
    {
        /// <summary>
        /// Convert file to file model.
        /// </summary>
        /// <param name="file">File info.</param>
        /// <returns>File model.</returns>
        public static FileModel ConvertToFileModel(this FileInfo file)
        {
            var fileModel = new FileModel();
            using (Stream outStream = new FileStream(file.FullName,
                                                     FileMode.Open,
                                                     FileAccess.Read,
                                                     FileShare.Read))
            {
                fileModel.Name = file.Name;
                fileModel.DataBytes = File.ReadAllBytes(file.FullName);
            }
            return fileModel;
        }
    }
}
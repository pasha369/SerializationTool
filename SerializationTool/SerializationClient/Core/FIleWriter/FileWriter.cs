using System.IO;
using System.Linq;
using SerializationClient.Models;

namespace SerializationClient.Core.FIleWriter
{
    public class FileWriter: IFileWriter
    {
        public void WriteFolderModel(FolderModel folder, string outFolderPath)
        {
            if (folder == null)
            {
                return;
            }
            var basePath = $"{outFolderPath}/{folder.Name}";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            if (folder.FileModels.Any())
            {
                foreach (var file in folder.FileModels)
                {
                    var filePath = $"{basePath}/{file.Name}";
                    File.WriteAllBytes(filePath, file.DataBytes);
                }
            }
            if (folder.SubFolderModels.Any())
            {
                foreach (var subFolder in folder.SubFolderModels)
                {
                    WriteFolderModel(subFolder, basePath);
                }
            }
        }

        public string WriteFileModel(string filePath, FileModel file)
        {
            if (file != null)
            {
                var fullName = Path.Combine(filePath, file.Name);
                File.WriteAllBytes(fullName, file.DataBytes);
                return fullName;
            }
            return string.Empty;
        }
    }
}

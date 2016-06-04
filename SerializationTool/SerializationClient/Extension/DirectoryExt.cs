using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerializationClient.Models;

namespace SerializationClient.Extension
{
    /// <summary>
    /// Extension for directory info.
    /// </summary>
    public static class DirectoryExt
    {
        /// <summary>
        /// Convert directory info to folder model.
        /// </summary>
        /// <param name="directoryInfo">Directory info</param>
        /// <returns>Folder model.</returns>
        public static FolderModel ConvertToFolderModel(this DirectoryInfo directoryInfo)
        {
            var fileModels = directoryInfo
                                .GetFiles()
                                .Select(x => x.ConvertToFileModel())
                                .ToList();
            var dirs = directoryInfo
                                .GetDirectories();
            var subFolderModels = new List<FolderModel>();
            if (dirs.Any())
            {
                subFolderModels = dirs
                                    .Select(x => x.ConvertToFolderModel())
                                    .ToList();
            }
            var folder = new FolderModel
            {
                Guid = Guid.NewGuid(), 
                Name = directoryInfo.Name,
                FileModels = fileModels,
                SubFolderModels = subFolderModels
            };

            return folder;
        }
    }
}

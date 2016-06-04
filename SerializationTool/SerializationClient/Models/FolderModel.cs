using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializationClient.Models
{
    /// <summary>
    /// Represents folder model.
    /// </summary>
    [Serializable]
    public class FolderModel
    {
        /// <summary>
        /// Gets or sets Guid.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets FileModels.
        /// </summary>
        public List<FileModel> FileModels { get; set; }

        /// <summary>
        /// Gets or sets SubFolderModels.
        /// </summary>
        public List<FolderModel> SubFolderModels { get; set; }

        public static FolderModel GetFolderModelByGuid(FolderModel model, Guid guid)
        {
            if (guid == model.Guid)
            {
                return model;
            }
            if (model.SubFolderModels.Any())
            {
                foreach (var subFolder in model.SubFolderModels)
                {
                    if (subFolder.Guid == guid)
                    {
                        return subFolder;
                    }
                    var result = GetFolderModelByGuid(subFolder, guid);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}

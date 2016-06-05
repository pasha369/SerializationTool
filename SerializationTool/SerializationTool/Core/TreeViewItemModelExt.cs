using System.IO;
using SerializationTool.Models;

namespace SerializationTool.Core
{
    /// <summary>
    /// Represent tree view item extenstion.
    /// </summary>
    public static class TreeViewItemModelExt
    {
        /// <summary>
        /// Convert to tree item model.
        /// </summary>
        /// <param name="dir">Directory info.</param>
        /// <returns>Tree view item model.</returns>
        public static TreeViewItemModel ConvertToTreeItemModel(this DirectoryInfo dir)
        {
            var treeViewItem = new TreeViewItemModel
            {
                Name = dir.Name,
                Path = dir.FullName
            };

            return treeViewItem;
        }
    }
}

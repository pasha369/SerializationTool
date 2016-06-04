using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerializationClient.Models;
using SerializationTool.Models;

namespace SerializationTool.Core
{
    public static class ExplorerItemExt
    {
        public static TreeViewItemModel ConvertToExplorerItem(this DirectoryInfo dir)
        {
            var treeViewItem = new TreeViewItemModel
            {
                Name = dir.Name,
                Path = dir.FullName,
                ChildItems = new List<TreeViewItemModel>()
            };

            try
            {
                if (dir.GetDirectories().Any())
                {
                    foreach (var current in dir.GetDirectories())
                    {
                        var childItem = current.ConvertToExplorerItem();
                        treeViewItem.ChildItems.Add(childItem);
                    }
                }
            }
            catch { }

            return treeViewItem;
        }
    }
}

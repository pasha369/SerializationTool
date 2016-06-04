using System;
using System.Collections.Generic;
using System.Linq;
using SerializationTool.Core;
using SerializationTool.ViewModels.Abstract;

namespace SerializationTool.Models
{
    /// <summary>
    /// Represents tree view item model.
    /// </summary>
    public class TreeViewItemModel : Observable
    {
        private bool _isSelected;

        /// <summary>
        /// Gets or sets Guid.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets IsRoot
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets Path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets ChildItems.
        /// </summary>
        public List<TreeViewItemModel> ChildItems { get; set; }

        /// <summary>
        /// Gets or sets IsSelected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SelectChild(this, value);

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Select/Unselect child items.
        /// </summary>
        /// <param name="treeViewItem">Parent item.</param>
        /// <param name="isSelected">Is selected?</param>
        /// <returns>Tree view item.</returns>
        public TreeViewItemModel SelectChild(TreeViewItemModel treeViewItem, bool isSelected)
        {
            if (treeViewItem.ChildItems.Any())
            {
                foreach (var item in treeViewItem.ChildItems)
                {
                    item.IsSelected = isSelected;
                }
            }

            return treeViewItem;
        }

        public static TreeViewItemModel GetFirstSelectedItem(TreeViewItemModel item)
        {
            if (item.IsSelected)
            {
                return item;
            }
            if(!item.IsSelected && item.ChildItems.Any())
            {
                foreach (var childItem in item.ChildItems)
                {
                    var selectedItem = GetFirstSelectedItem(childItem);
                    if (selectedItem != null)
                    {
                        return selectedItem;
                    }
                }
            }
            return null;
        }
    }
}

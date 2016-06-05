using System;
using System.Collections.ObjectModel;
using System.IO;
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
        private bool _isExpanded;

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
        /// Gets or sets IsExpanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged();

                LoadChildItems(this, 1, 0);
            }
        }

        /// <summary>
        /// Gets or sets Path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets ChildItems.
        /// </summary>
        public ObservableCollection<TreeViewItemModel> ChildItems { get; set; }

        /// <summary>
        /// Gets or sets IsSelected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                OnPropertyChanged();
            }
        }

        public TreeViewItemModel()
        {
            ChildItems = new ObservableCollection<TreeViewItemModel>();
            ChildItems.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(ChildItems));
        }

        /// <summary>
        /// Load child items.
        /// </summary>
        /// <param name="item">Current tree view item.</param>
        /// <param name="level">Recursion level.</param>
        /// <param name="index">Current level.</param>
        public void LoadChildItems(TreeViewItemModel item, int? level = null, int? index = null)
        {
            try
            {
                var dir = new DirectoryInfo(Path);
                var folders = dir
                    .GetDirectories()
                    .Select(x => x.ConvertToTreeItemModel())
                    .ToList();

                item.ChildItems.Clear();
                foreach (var folder in folders)
                {
                    if (level.HasValue && level.Value >= index.Value)
                    {
                        index++;
                        LoadChildItems(folder, level, index);
                    }
                    item.ChildItems.Add(folder);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Get selected item.
        /// </summary>
        /// <param name="item">Root item.</param>
        /// <returns>Selected item.</returns>
        public static TreeViewItemModel GetSelectedItem(TreeViewItemModel item)
        {
            if (item.IsSelected)
            {
                return item;
            }
            if (!item.IsSelected && item.ChildItems.Any())
            {
                var selectedTreeItemModel = item.ChildItems
                           .Select(childItem => GetSelectedItem(childItem))
                           .FirstOrDefault(selectedItem => selectedItem != null);
                if (selectedTreeItemModel != null)
                {
                    selectedTreeItemModel.IsSelected = false;
                    return selectedTreeItemModel;
                }
            }
            return null;
        }
    }
}

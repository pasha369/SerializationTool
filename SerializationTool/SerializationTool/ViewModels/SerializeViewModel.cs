using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using SerializationClient;
using SerializationLogger.Abstract;
using SerializationTool.Commands;
using SerializationTool.Core;
using SerializationTool.Models;
using SerializationTool.ViewModels.Abstract;

namespace SerializationTool.ViewModels
{
    /// <summary>
    /// Represents serialize page view model.
    /// </summary>
    public class SerializeViewModel : Observable, IPageViewModel
    {
        private const string SerializePageName = "Serialize";
        private const string SerializeIcon = "../../Resources/Images/zip.png";

        private string[] _logicalDrives;
        private string _selectedLogicalDrive;
        private TreeViewItemModel _selectedItem;
        private ObservableCollection<TreeViewItemModel> _treeItems;
        private SerializeClientWrapper _serializeClientWrapper;
        private ISerializeLogger _logger;
        private bool _isExpanded;
        private bool _isSelected;

        /// <summary>
        /// Initialize SerializeViewModel instance.
        /// </summary>
        /// <param name="serializeClientWrapper">Serialize client.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        public SerializeViewModel(SerializeClientWrapper serializeClientWrapper, ILoggerFactory loggerFactory)
        {
            _serializeClientWrapper = serializeClientWrapper;
            _logger = loggerFactory.GetLogger("main");

            _treeItems = new ObservableCollection<TreeViewItemModel>();
            TreeItems.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(TreeItems));

            _logicalDrives = Environment.GetLogicalDrives();
            SelectedLogicalDrive = LogicalDrives.First();
        }

        /// <summary>
        /// Gets or sets SelectedLogicalDrive.
        /// </summary>
        public string SelectedLogicalDrive
        {
            get { return _selectedLogicalDrive; }
            set
            {
                _selectedLogicalDrive = value;
                OnPropertyChanged();

                GetItemList();
            }
        }

        /// <summary>
        /// Gets or sets LogicalDrives.
        /// </summary>
        public string[] LogicalDrives
        {
            get { return _logicalDrives; }
            set { _logicalDrives = value; }
        }

        /// <summary>
        /// Gets or sets TreeItems.
        /// </summary>
        public ObservableCollection<TreeViewItemModel> TreeItems
        {
            get { return _treeItems; }
        }

        /// <summary>
        /// Gets or sets SelectedItem.
        /// </summary>
        public TreeViewItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets page name.
        /// </summary>
        public string Name
        {
            get { return SerializePageName; }
        }

        /// <summary>
        /// Gets or sets page icon.
        /// </summary>
        public string IconPath
        {
            get { return SerializeIcon; }
        }

        /// <summary>
        /// Gets or sets SelectedItem.
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

        /// <summary>
        /// Gets HandleSerializeCommand.
        /// </summary>
        public ICommand HandleSerializeCommand
        {
            get { return new RelayCommand(p => SerializeSelectedFolder());}
        }

        private async void SerializeSelectedFolder()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Document"; 
            dialog.DefaultExt = ".bin";
            dialog.Filter = "Binary file (.bin)|*.bin"; 

            bool? result = dialog.ShowDialog();
            
            if (result == true)
            {
                string filename = dialog.FileName;

                _serializeClientWrapper.SerializedFilePath = filename;
                await _serializeClientWrapper.SerializeFolderAsync(SelectedItem.Path);
            }
        }

        private void GetItemList()
        {
            try
            {
                TreeItems.Clear();

                var dir = new DirectoryInfo(SelectedLogicalDrive);

                var folders = dir
                    .GetDirectories()
                    .Select(x => x.ConvertToTreeItemModel())
                    .ToList();

                foreach (var folder in folders)
                {
                    folder.LoadChildItems(folder);
                    TreeItems.Add(folder);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

    }
}
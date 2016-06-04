using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using SerializationClient;
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

        private TreeViewItemModel _selectedItem;

        private ObservableCollection<TreeViewItemModel> _treeItems;
        private SerializeClientWrapper _serializeClientWrapper;


        public SerializeViewModel(SerializeClientWrapper serializeClientWrapper)
        {
            _serializeClientWrapper = serializeClientWrapper;

            _treeItems = new ObservableCollection<TreeViewItemModel>();
            TreeItems.CollectionChanged += ExplorelItemList_CollectionChanged;
            InitItemList();
        }

        public ObservableCollection<TreeViewItemModel> TreeItems
        {
            get { return _treeItems; }
        }

        public TreeViewItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return SerializePageName; }
        }

        public string IconPath
        {
            get { return "../../Resources/Images/zip.png"; }
        }

        private void ExplorelItemList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TreeItems));
        }

        public ICommand HandleSerializeCommand
        {
            get { return new DelegateCommand(SerializeSelectedFolder);}
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

        private void InitItemList()
        {
            var dir = new DirectoryInfo(@"D:\");
            var folders = dir
                .GetDirectories()
                .Select(x => ExplorerItemExt.ConvertToExplorerItem(x))
                .ToList();

            foreach (var folder in folders)
            {
                TreeItems.Add(folder);
            }
        }

    }
}
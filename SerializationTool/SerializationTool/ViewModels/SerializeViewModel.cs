using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using SerializationClient;
using SerializationClient.Core.FIleWriter;
using SerializationClient.Core.SerializeClients;
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

        public TreeViewItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TreeViewItemModel> _treeItems;

        public ObservableCollection<TreeViewItemModel> TreeItems
        {
            get { return _treeItems; }
        }

        private void ExplorelItemList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TreeItems));
        }

        public SerializeViewModel()
        {
            _treeItems = new ObservableCollection<TreeViewItemModel>();
            TreeItems.CollectionChanged += ExplorelItemList_CollectionChanged;
            InitItemList();
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

        public ICommand HandleSerializeCommand
        {
            get { return new DelegateCommand(SerializeSelectedFolder);}
        }

        private void SerializeSelectedFolder()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".bin";
            dlg.Filter = "Binary file (.bin)|*.bin"; 
            // Show save file dialog box
            bool? result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;

                SerializeClientWrapper clientWrapper = new SerializeClientWrapper();
                clientWrapper.FileWriter = new FileWriter();
                clientWrapper.SerializeClient = new BinarySerializeClient();
                clientWrapper.SerializedFilePath = filename;
                clientWrapper.SerializeFolder(SelectedItem.Path);
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
    }
}
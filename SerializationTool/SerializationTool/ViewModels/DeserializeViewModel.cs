using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using AutoMapper;
using SerializationClient;
using SerializationClient.Core.FIleWriter;
using SerializationClient.Core.SerializeClients;
using SerializationClient.Models;
using SerializationTool.Commands;
using SerializationTool.Models;
using SerializationTool.ViewModels.Abstract;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace SerializationTool.ViewModels
{
    /// <summary>
    /// Represents deserialize page view model.
    /// </summary>
    public class DeserializeViewModel : Observable, IPageViewModel
    {
        private const string DeserializePageName = "Deserialize";

        private ICommand _openFileCommand;
        private ICommand _handleDeserializeCommand;

        private SerializeClientWrapper _serializeClient;
        private ObservableCollection<TreeViewItemModel> _treeViewItemModels;
        private string _outputPath;
        private FolderModel _folder;
        private string _folderName;

        public string FolderName
        {
            private set
            {
                _folderName = value;
                OnPropertyChanged();
            }
            get { return _folderName; }
        }

        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                _outputPath = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TreeViewItemModel> TreeViewItemModels
        {
            get { return _treeViewItemModels; }
        }

        public DeserializeViewModel(SerializeClientWrapper serializeClientWrapper)
        {
            _serializeClient = serializeClientWrapper;

            _treeViewItemModels = new ObservableCollection<TreeViewItemModel>();
            _treeViewItemModels.CollectionChanged += TreeViewItemModels_CollectionChanged;
        }

        private void TreeViewItemModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TreeViewItemModels));
        }

        public string Name
        {
            get { return DeserializePageName; }
        }

        public string IconPath
        {
            get { return "../../Resources/Images/unzip.png"; }
        }

        public ICommand HandleDeserializeCommand
        {
            get
            {
                if (_handleDeserializeCommand == null)
                {
                    _handleDeserializeCommand = new RelayCommand(p => HandleDeserialize());
                }
                return _handleDeserializeCommand;
            }
        }

        private void HandleDeserialize()
        {
            var dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var selectedItem = TreeViewItemModel.GetFirstSelectedItem(TreeViewItemModels.First());
                if (selectedItem != null)
                {
                    var folder = FolderModel.GetFolderModelByGuid(_folder, selectedItem.Guid);
                    _serializeClient.SaveFolder(folder, dialog.SelectedPath);
                }
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                if (_openFileCommand == null)
                {
                    _openFileCommand = new RelayCommand(
                        p => OpeFile());
                }

                return _openFileCommand;
            }
        }

        public void OpeFile()
        {
            // Create OpenFileDialog 
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Binary file (.bin)|*.bin";
            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string filename = dialog.FileName;
                _folder = _serializeClient.DeserializeFolderModel(filename);
                var treeItem = Mapper.Map<TreeViewItemModel>(_folder);
                FolderName = filename;

                TreeViewItemModels.Clear();
                TreeViewItemModels.Add(treeItem);
            }
        }
    }
}
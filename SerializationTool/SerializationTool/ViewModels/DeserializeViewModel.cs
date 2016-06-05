using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using AutoMapper;
using SerializationClient;
using SerializationClient.Models;
using SerializationLogger.Abstract;
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
        private const string DeserializeIcon = "../../Resources/Images/unzip.png";

        private ICommand _openFileCommand;
        private ICommand _handleDeserializeCommand;

        private SerializeClientWrapper _serializeClient;
        private ObservableCollection<TreeViewItemModel> _treeViewItemModels;
        private FolderModel _folder;
        private string _filename;
        private ISerializeLogger _logger;
        private bool _isSelected;

        /// <summary>
        /// Initialize DeserializeViewModel instance. 
        /// </summary>
        /// <param name="serializeClientWrapper">Serialize client.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        public DeserializeViewModel(SerializeClientWrapper serializeClientWrapper, ILoggerFactory loggerFactory)
        {
            _serializeClient = serializeClientWrapper;
            _logger = loggerFactory.GetLogger("main");

            _treeViewItemModels = new ObservableCollection<TreeViewItemModel>();
            _treeViewItemModels.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(TreeViewItemModels));
        }

        /// <summary>
        /// Gets or sets Filename.
        /// </summary>
        public string Filename
        {
            private set
            {
                _filename = value;
                OnPropertyChanged();
            }
            get { return _filename; }
        }

        /// <summary>
        /// Gets TreeViewItemModels.
        /// </summary>
        public ObservableCollection<TreeViewItemModel> TreeViewItemModels
        {
            get { return _treeViewItemModels; }
        }

        /// <summary>
        /// Gets page name.
        /// </summary>
        public string Name
        {
            get { return DeserializePageName; }
        }

        /// <summary>
        /// Gets IconPath.
        /// </summary>
        public string IconPath
        {
            get { return DeserializeIcon; }
        }

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

        /// <summary>
        /// Gets HandleDeserializeCommand.
        /// </summary>
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

        /// <summary>
        /// Gets OpenFileCommand.
        /// </summary>
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

        /// <summary>
        /// Open file using open dialog.
        /// </summary>
        public void OpeFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Binary file (.bin)|*.bin";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                try
                {
                    _folder = _serializeClient.DeserializeFolderModel(filename);
                    var treeItem = Mapper.Map<TreeViewItemModel>(_folder);
                    Filename = filename;

                    TreeViewItemModels.Clear();
                    TreeViewItemModels.Add(treeItem);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        private void HandleDeserialize()
        {
            var dialog = new FolderBrowserDialog();

            try
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var rootTreeItem = TreeViewItemModels.First();
                    var selectedItem = TreeViewItemModel.GetSelectedItem(rootTreeItem);

                    if (selectedItem != null)
                    {

                        var folder = FolderModel.GetFolderModelByGuid(_folder, selectedItem.Guid);
                        _serializeClient.SaveFolder(folder, dialog.SelectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Autofac;
using SerializationTool.Commands;
using SerializationTool.Startup;
using SerializationTool.ViewModels.Abstract;

namespace SerializationTool.ViewModels
{
    /// <summary>
    /// Represents navigation view model.
    /// </summary>
    public class NavigationViewModel : Observable
    {
        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public NavigationViewModel()
        {
            _pageViewModels = new List<IPageViewModel>();

            _pageViewModels.Add(DependencyResolver.Current.Resolve<SerializeViewModel>());
            _pageViewModels.Add(DependencyResolver.Current.Resolve<DeserializeViewModel>());
            
            _currentPageViewModel = _pageViewModels.First();
            CurrentPageViewModel.IsSelected = true;
        }

        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged();
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels; }
            set { _pageViewModels = value; }
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeCurrentPage((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        private void ChangeCurrentPage(IPageViewModel pageViewModel)
        {
            if (CurrentPageViewModel != null)
            {
                CurrentPageViewModel.IsSelected = false;
            }
            CurrentPageViewModel = PageViewModels.Single(vm => vm == pageViewModel);
            CurrentPageViewModel.IsSelected = true;
        }
    }
}

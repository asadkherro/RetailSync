using RetailSync.Core;
using RetailSync.Views.Main;

namespace RetailSync.ViewModels.Main
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            CurrentView = new DashBoardView();
        }
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChange(); }
        }
        private object _currentView;


        public RelayCommand DashBoardCommand => new RelayCommand(_ => CurrentView = new DashBoardView() );
        public RelayCommand ProductCommand => new RelayCommand(_ => CurrentView = new ProductsView() );

    }
}

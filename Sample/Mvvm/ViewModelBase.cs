using System.Threading.Tasks;

namespace Sample.Mvvm
{
    public class ViewModelBase: NotificationObject
    {
        //private LoaderViewModel _loader;
        //public LoaderViewModel Loader
        //{
        //    get => _loader;
        //    set => SetProperty(ref _loader, value);
        //}

        private int _busyCounter;
        protected int BusyCounter
        {
            get => _busyCounter;
            set
            {
                _busyCounter = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        public bool IsBusy
        {
            get => BusyCounter > 0;
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ViewModelBase()
        {
            //_loader = new LoaderViewModel();
        }

        #region Life cycle

        public virtual Task OnAppearing()
        {
            return InitAsync();
        }

        protected virtual Task InitAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearing()
        {
            //_loader.CancelExecutions();

            return Task.CompletedTask;
        }

        #endregion
    }
}

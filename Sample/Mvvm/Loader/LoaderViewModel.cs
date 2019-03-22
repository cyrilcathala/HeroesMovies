using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Sample.Helpers;

namespace Sample.Mvvm
{
    public class LoaderViewModel : NotificationObject, ILoaderViewModel
    {
        private LoaderState _state;
        public LoaderState State
        {
            get => _state;
            set
            {
                SetProperty(ref _state, value);
                RaisePropertyChanged(nameof(IsCompleted));
                RaisePropertyChanged(nameof(IsBusy));
                RaisePropertyChanged(nameof(IsNotBusy));
                RaisePropertyChanged(nameof(IsNotCompleted));
                RaisePropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool IsCompleted => State == LoaderState.Completed;
        public bool IsNotCompleted => !IsCompleted;
        public bool IsBusy => State == LoaderState.Loading;
        public bool IsNotBusy => !IsBusy;
        public bool IsRefreshing => State == LoaderState.Refreshing;

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        private string _emptyMessage;
        public string EmptyMessage
        {
            get => _emptyMessage;
            private set => SetProperty(ref _emptyMessage, value);
        }

        private readonly Command _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand;

        private Func<CancellationToken, Task<ILoaderResult>> _loadTask;

        private readonly TaskRunner _taskRunner;

        public LoaderViewModel()
        {
            _taskRunner = new TaskRunner();

            _refreshCommand = new Command(() => Retry());
        }

        public async Task Execute(Func<CancellationToken, Task<ILoaderResult>> loadTask, bool cancelPreviousTasks = true)
        {
            if (cancelPreviousTasks)
                CancelExecutions();

            _loadTask = loadTask;

            try
            {
                State = LoaderState.Loading;

                var result = await _taskRunner.ExecuteAsync(loadTask);

                if (!result.IsSuccess)
                {
                    ErrorMessage = result.ErrorMessage;

                    State = LoaderState.Faulted;
                    return;
                }

                if (result.IsEmpty)
                {
                    EmptyMessage = result.EmptyMessage;
                    State = LoaderState.Empty;
                    return;
                }

                State = LoaderState.Completed;
            }
            catch (OperationCanceledException)
            {
                // Edge case: silently ignore the exception
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoaderViewModel - Exception: {ex.Message}" + Environment.NewLine + ex.StackTrace);
                State = LoaderState.Faulted;
                throw;
            }
        }

        public async Task Refresh(Func<CancellationToken, Task<ILoaderResult>> loadTask = null)
        {
            loadTask = loadTask ?? _loadTask;
            if (loadTask == null || State != LoaderState.Completed)
            {
                return;
            }

            var oldState = State;
            State = LoaderState.Refreshing;

            try
            {
                var result = await _taskRunner.ExecuteAsync(loadTask);

                if (!result.IsSuccess)
                {
                    // TODO
                }
            }
            catch (OperationCanceledException)
            {
                // Silently ignore the exception
            }

            State = LoaderState.Completed;
        }

        public Task Retry()
        {
            if (_loadTask == null) return Task.CompletedTask;

            return Execute(_loadTask);
        }

        public void CancelExecutions()
        {
            if (_loadTask == null) return;

            _taskRunner.CancelExecutions();
        }
    }
}
